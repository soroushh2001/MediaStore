using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Cart.Commands.IncreaseDecreaseCartItem
{
    public record IncreaseDecreaseCartItemCommand(int OrderDetailId,string Op):IRequest<bool>;
 
}
