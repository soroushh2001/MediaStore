using MediaStore.Application.Specifications;
using MediatR;

namespace MediaStore.Application.Features.Products.Queries.GetFilteredProducts
{
    public record GetFilteredProductsQuery(FilterProductSpecification Specification) : IRequest<FilteredProductsResponse>;
}
