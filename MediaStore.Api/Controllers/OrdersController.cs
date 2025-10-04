using MediaStore.Application.Features.Cart.Commands.AddToCart;
using MediaStore.Application.Features.Cart.Commands.DeleteFromCart;
using MediaStore.Application.Features.Cart.Commands.IncreaseDecreaseCartItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-to-cart"),Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToCart(AddToCartCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("delete-from-cart/{OrderDetailId}"),Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFromCart([FromRoute]DeleteFromCartCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("increase-decrease-cart-item"),Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IncreaseDecreaseCartItem(IncreaseDecreaseCartItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
