using MediaStore.Application.Features.Categories.Shared;
using MediatR;

namespace MediaStore.Application.Features.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery(bool? isDeleted) : IRequest<List<CategoryResponse>>;
}
