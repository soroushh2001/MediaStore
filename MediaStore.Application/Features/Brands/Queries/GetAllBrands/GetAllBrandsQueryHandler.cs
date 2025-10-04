using AutoMapper;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Brands.Shared;
using MediatR;

namespace MediaStore.Application.Features.Brands.Queries.GetAllBrands
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, List<BrandResponse>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public GetAllBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<List<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetAllBrandsAsync(request.IsDeleted);
            return _mapper.Map<List<BrandResponse>>(brands);
        }
    }

}
