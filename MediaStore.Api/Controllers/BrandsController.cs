using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Brands.Commands.CreateBrand;
using MediaStore.Application.Features.Brands.Commands.DeleteBrand;
using MediaStore.Application.Features.Brands.Commands.UpdateBrand;
using MediaStore.Application.Features.Brands.Queries.GetAllBrands;
using MediaStore.Application.Features.Brands.Queries.GetBrandById;
using MediaStore.Application.Features.Brands.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MediaStore.Api.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBrands([FromQuery] bool? isDeleted)
        {
            return Ok(await _mediator.Send(new GetAllBrandsQuery(isDeleted)));
        }

        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<BrandResponse>>> GetBrandById([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new GetBrandByIdQuery(id)));
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateBrand(CreateBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBrand([FromBody] UpdateBrandCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("soft-delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteBrand([FromRoute] int id)
        {
           return Ok(await _mediator.Send(new DeleteBrandCommand(id)));
        }
    }
}
