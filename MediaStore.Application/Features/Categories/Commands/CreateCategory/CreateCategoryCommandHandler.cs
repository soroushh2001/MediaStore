using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Application.StaticDetails;
using MediaStore.Domain.Entities;
using MediatR;

namespace MediaStore.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();
            var validator = new CreateCategoryCommandValidator(_categoryRepository);
            var validationResult = await validator.ValidateAsync(request,cancellationToken);
            if (!validationResult.IsValid)
            {
                response.StatusCode = StatusCodes.BadRequest;
                response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            request.Slug = request.Slug.GenerateSlug();
            var category = _mapper.Map<Category>(request);

            await _categoryRepository.CreateCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return response;
        }
    }
}
