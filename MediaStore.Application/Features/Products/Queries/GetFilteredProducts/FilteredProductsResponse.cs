using MediaStore.Application.Common.Responses;
using MediaStore.Application.Specifications;
using MediaStore.Domain.Entities;

namespace MediaStore.Application.Features.Products.Queries.GetFilteredProducts
{
    public class FilteredProductsResponse
    {
        public PaginatedResponse<ProductListItemResponse>? Products { get; set; }
        public FilterProductSpecification? Specification { get; set; }  
    }
}
