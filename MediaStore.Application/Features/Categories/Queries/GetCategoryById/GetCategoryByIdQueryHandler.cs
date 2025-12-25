using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Categories.Shared;
using MediaStore.Application.StaticDetails;
using MediatR;

namespace MediaStore.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<CategoryResponse>();
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                apiResponse.StatusCode = StatusCodes.NotFound;
                apiResponse.IsSuccess = false;
                apiResponse.Data = null;
                return apiResponse;
            }

            apiResponse.Data = _mapper.Map<CategoryResponse>(category);
            return apiResponse;
        }
    }
}
