using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Brands.Shared;
using MediaStore.Application.StaticDetails;
using MediatR;

namespace MediaStore.Application.Features.Brands.Queries.GetBrandById
{
    public class BrandQueryHandler : IRequestHandler<GetBrandByIdQuery, ApiResponse<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public BrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BrandResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<BrandResponse>();
            var brand = await _brandRepository.GetByIdAsync(request.Id);
            if(brand == null)
            {
                apiResponse.StatusCode = StatusCodes.NotFound;
                return apiResponse;
            }
            
            apiResponse.Data = _mapper.Map<BrandResponse>(brand);
            return apiResponse;
        }
    }
}
