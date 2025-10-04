using MediaStore.Application.Common.Requests;
using MediaStore.Application.Common.Responses;

namespace MediaStore.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterRequest request);
        Task<ApiResponse<AuthResponse>> AuthorizeAsync(AuthRequest request);
    }
}
