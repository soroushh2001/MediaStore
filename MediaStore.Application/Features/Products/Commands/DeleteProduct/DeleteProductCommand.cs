﻿using MediaStore.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int Id):IRequest<ApiResponse<bool>>;
}
