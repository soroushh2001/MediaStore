using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Categories.Shared;
using MediatR;

namespace MediaStore.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : BaseCategoryCommand,IRequest<ApiResponse<bool>>
    {
        public int? ParentId { get; set; }
    }
}
