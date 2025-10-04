using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Brands.Shared;
using MediaStore.Application.StaticDetails;
using MediaStore.Domain.Entities;
using MediatR;

namespace MediaStore.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery,ApiResponse<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<ProductResponse>();
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            if(product == null)
            {
                apiResponse.StatusCode = StatusCodes.NotFound;
                return apiResponse;

            }
            apiResponse.Data = _mapper.Map<ProductResponse>(product);
            return apiResponse;

        }
    }
}
