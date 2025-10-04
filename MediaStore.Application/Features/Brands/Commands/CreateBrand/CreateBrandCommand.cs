using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Brands.Shared;
using MediatR;

namespace MediaStore.Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : BaseBrandCommand, IRequest<ApiResponse<bool>>
    {
    }
}
