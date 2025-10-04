using MediaStore.Application.Common.Requests;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaStore.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegisterResponse>>> Register(RegisterRequest register)
        {
            return Ok(await _authService.RegisterAsync(register));
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize(AuthRequest request)
        {
            return Ok(await _authService.AuthorizeAsync(request));
        }
    }
}
