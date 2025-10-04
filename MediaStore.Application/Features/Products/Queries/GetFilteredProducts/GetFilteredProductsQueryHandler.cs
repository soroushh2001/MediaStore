using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Domain.Entities;
using MediatR;

namespace MediaStore.Application.Features.Products.Queries.GetFilteredProducts
{
    public class GetFilteredProductsQueryHandler : IRequestHandler<GetFilteredProductsQuery, FilteredProductsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetFilteredProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<FilteredProductsResponse> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            PaginatedResponse<Product> filteredProducts = await _productRepository.GetFilteredProductsAsync(request.Specification);
            var items = _mapper.Map<List<ProductListItemResponse>>(filteredProducts.Items);
            return new FilteredProductsResponse()
            {
                Products = new()
                {
                    VisiblePages = filteredProducts.VisiblePages,
                    Items = items,
                    HasNextPage = filteredProducts.HasNextPage,
                    HasPreviousPage = filteredProducts.HasPreviousPage,
                },
                Specification = request.Specification
            };
        }
    }
}
