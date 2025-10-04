using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Application.StaticDetails;
using MediatR;

namespace MediaStore.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();
            var categoryToUpdate = await _categoryRepository.GetCategoryByIdAsync(request.Id);

            if (categoryToUpdate == null)
            {
                response.StatusCode = StatusCodes.NotFound;
                response.IsSuccess = false;
                return response;
            }

            var validator = new UpdateCategoryCommandValidator(_categoryRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.StatusCode = StatusCodes.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }

            request.Slug = request.Slug.GenerateSlug();
            categoryToUpdate = _mapper.Map(request, categoryToUpdate);

            categoryToUpdate.LastModifiedDate = DateTime.Now;

            _categoryRepository.UpdateCategory(categoryToUpdate);
            await _categoryRepository.SaveChangesAsync();
            return response;
        }
    }
}
