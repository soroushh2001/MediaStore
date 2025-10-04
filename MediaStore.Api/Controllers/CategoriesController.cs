using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Categories.Commands.CreateCategory;
using MediaStore.Application.Features.Categories.Commands.DeleteCategory;
using MediaStore.Application.Features.Categories.Commands.UpdateCategory;
using MediaStore.Application.Features.Categories.Queries.GetAllCategories;
using MediaStore.Application.Features.Categories.Queries.GetCategoryById;
using MediaStore.Application.Features.Categories.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaStore.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories(bool? isDeleted)
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery(isDeleted)));
        }

        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<CategoryResponse>>> GetCategoryById(int id)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery(id)));
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateCategory(CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }


        [HttpPut("edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("soft-delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCategory([FromRoute] int id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryCommand(id)));
        }

    }
}
