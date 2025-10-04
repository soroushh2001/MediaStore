using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Products.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MediaStore.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : BaseProductCommand,IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
    }
}
