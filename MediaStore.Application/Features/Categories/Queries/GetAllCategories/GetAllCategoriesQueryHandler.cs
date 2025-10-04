using AutoMapper;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Categories.Shared;
using MediatR;

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
            var categories = await _categoryRepository.GetAllCategoriesAsync(request.isDeleted);
            return _mapper.Map<List<CategoryResponse>>(categories);
        }
    }
}
