using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Brands.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommand : BaseBrandCommand, IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
    }
}
