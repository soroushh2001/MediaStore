using MediaStore.Application.Common.Requests;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Identity;
using MediaStore.Application.StaticDetails;
using MediaStore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ApiResponse<RegisterResponse>> RegisterAsync(Application.Common.Requests.RegisterRequest request)
        {
            var response = new ApiResponse<RegisterResponse>();
            var user = new ApplicationUser()
            {
                Email = request.Email,
                EmailConfirmed = true,
                UserName = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                response.Data = new() { UserId = user.Id };
                return response;
            }
            response.IsSuccess = false;
            response.StatusCode = StatusCodes.BadRequest;
            response.ErrorMessages = result.Errors.Select(x => x.Description).ToList();
            return response;
        }

        public async Task<ApiResponse<AuthResponse>> AuthorizeAsync(AuthRequest request)
        {
            var response = new ApiResponse<AuthResponse>();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.BadRequest;
                response.ErrorMessages.Add("کاربری با این مشخصات یافت نشد");
                return response;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.BadRequest;
                response.ErrorMessages.Add("کاربری با این مشخصات یافت نشد");
                return response;
            }

            await _userManager.AddToRoleAsync(user, "User");
            JwtSecurityToken jwtSecurityToken = await GenerateTokenAsync(user);

            response.Data = new AuthResponse()
            {
                Email = user.Email,
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            return response;
        }

        private async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("UserId", user.Id)
                }.Union(userClaims)
                .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );
            return jwtSecurityToken;
        }

    }
}
