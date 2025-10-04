using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Categories.Shared;
using MediatR;

namespace MediaStore.Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(int Id) : IRequest<ApiResponse<CategoryResponse>>;
}
