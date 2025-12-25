using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.StaticDetails;
using MediatR;

namespace MediaStore.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                response.StatusCode = StatusCodes.NotFound;
                return response;
            }
            category.IsDeleted = true;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            return response;
        }
    }
}
