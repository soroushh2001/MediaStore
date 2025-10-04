using MediatR;

namespace MediaStore.Application.Features.Cart.Commands.DeleteFromCart
{
    public record DeleteFromCartCommand(int OrderDetailId) : IRequest<bool>;
}
