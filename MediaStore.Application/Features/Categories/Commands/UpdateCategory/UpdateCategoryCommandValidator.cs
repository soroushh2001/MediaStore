
using FluentValidation;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Categories.Commands.CreateCategory;
using MediaStore.Application.Features.Categories.Shared;

namespace MediaStore.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryCommandValidator(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;

            Include(new BaseCategoryValidator());

            RuleFor(c => c)
                .MustAsync(CheckCategorySlugExisted)
                .WithMessage("این اسلاگ قبلا ثبت شده است.");
            RuleFor(c => c)
              .MustAsync(CheckCategoryTitleExisted)
              .WithMessage("این عنوان قبلا ثبت شده است.");

        }

        private async Task<bool> CheckCategorySlugExisted(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var check = await _categoryRepository.CheckCategorySlugExisted(command.Slug, command.Id);
            return !check;
        }

        private async Task<bool> CheckCategoryTitleExisted(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var check = await _categoryRepository.CheckCategoryTitleExisted(command.Title, command.Id);
            return !check;
        }
    }
}
