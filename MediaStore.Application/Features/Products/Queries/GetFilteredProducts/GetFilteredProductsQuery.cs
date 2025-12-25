using MediaStore.Application.Common.Responses;
using MediaStore.Application.FilterParameters;
using MediatR;

namespace MediaStore.Application.Features.Products.Queries.GetFilteredProducts
{
    public record GetFilteredProductsQuery(BaseQueryParameters BaseQueryParams,FilterProductParameters FilterParams) : IRequest<PaginatedResponse<ProductListItemResponse>>;
}
