using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Categories.Shared;
using MediatR;

namespace MediaStore.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : BaseCategoryCommand, IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
    }
}
