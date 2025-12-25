using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Application.Features.Products.Queries.GetFilteredProducts
{
    public class GetFilteredProductsQueryHandler : IRequestHandler<GetFilteredProductsQuery, PaginatedResponse<ProductListItemResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetFilteredProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<ProductListItemResponse>> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var query = _productRepository.GetQuryable();

            if (request.FilterParams.BrandSlugs != null)
            {
                query = query.Where(p => request.FilterParams.BrandSlugs.Contains(p.Brand.Slug));
            }

            if (!string.IsNullOrEmpty(request.FilterParams.CategorySlug))
            {
                query = query.Where(p => p.ProductCategories != null &&
                p.ProductCategories.Any(pc => pc.Category.Slug == request.FilterParams.CategorySlug));
            }

            if (!string.IsNullOrEmpty(request.FilterParams.Search))
            {
                request.BaseQueryParams.SortBy = "Title";
                query = query.Where(p => p.Title.Contains(request.FilterParams.Search))
                    .OrderByDescending(p => p.Title.StartsWith(request.FilterParams.Search) ? 1 : 0);
            }

            else
            {
                if (!string.IsNullOrEmpty(request.BaseQueryParams.SortBy))
                {
                    var orderCondition = request.BaseQueryParams.OrderBy == "Desc";


                    switch (request.BaseQueryParams.SortBy)
                    {
                        case "ModifiedDate":
                            query = orderCondition
                                ? query.OrderByDescending(x => x.LastModifiedDate)
                                : query.OrderBy(x => x.LastModifiedDate);
                            break;
                        case "Price":
                            query = orderCondition ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price);
                            break;
                        case "Title":
                            query = orderCondition ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title);
                            break;
                    }
                }
            }

            var items = query
        .ProjectTo<ProductListItemResponse>(_mapper.ConfigurationProvider);

            return await PaginatedList<ProductListItemResponse>.CreateAsync(items,request.BaseQueryParams.PageIndex,request.BaseQueryParams.PageSize,request.BaseQueryParams.PageRange);

        }
    }
}
