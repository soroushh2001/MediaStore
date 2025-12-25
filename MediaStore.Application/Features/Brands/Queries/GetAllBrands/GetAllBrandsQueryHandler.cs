using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Brands.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var query = _brandRepository.Query();

            if (request.IsDeleted != null)
            {
                query = query.Where(b => b.IsDeleted == request.IsDeleted.Value);
            }
            
            return await query.ProjectTo<BrandResponse>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }

}
