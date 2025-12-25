using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Categories.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery,List<CategoryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query =  _categoryRepository.Query();
            if(request.isDeleted != null)
                query = query.Where(x => x.IsDeleted == request.isDeleted.Value);

            return await query.ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
