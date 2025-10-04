using MediaStore.Application.Common.Responses;
using MediatR;

namespace MediaStore.Application.Features.Brands.Commands.DeleteBrand
{
    public record DeleteBrandCommand(int Id) : IRequest<ApiResponse<bool>>;
}
