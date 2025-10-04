using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Products.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : BaseProductCommand,IRequest<ApiResponse<bool>>
    {
        public IFormFile Image { get; set; } = null!;
    }
}
