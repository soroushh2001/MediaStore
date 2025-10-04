using MediaStore.Application.Common.Responses;
using MediatR;

namespace MediaStore.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest<ApiResponse<bool>>;
}
