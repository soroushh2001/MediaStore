using MediatR;

namespace MediaStore.Application.Features.Cart.Commands.AddToCart
{
    public record AddToCartCommand(int ProductId,int Count = 1) : IRequest<Unit>;
}
