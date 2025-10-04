using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Products.Commands.CreateProduct;
using MediaStore.Application.Features.Products.Commands.DeleteProduct;
using MediaStore.Application.Features.Products.Commands.UpdateProduct;
using MediaStore.Application.Features.Products.Queries.GetFilteredProducts;
using MediaStore.Application.Features.Products.Queries.GetProductById;
using MediaStore.Application.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaStore.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-filtered")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery]FilterProductSpecification specification)
        {
            return Ok(await _mediator.Send(new GetFilteredProductsQuery(specification)));
        }

        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> GetProductById(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery(id)));
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateProduct([FromForm]CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProduct([FromForm]UpdateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("soft-delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(int id)
        {
            return Ok(await _mediator.Send(new DeleteProductCommand(id)));
        }
    }
}
