using FluentValidation;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Brands.Commands.CreateBrand;
using MediaStore.Application.Features.Brands.Shared;
using MediaStore.Application.Features.Categories.Shared;

namespace MediaStore.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
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

        private async Task<bool> CheckCategorySlugExisted(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var check = await _categoryRepository.CheckSlugExisted(command.Slug);
            return !check;
        }

        private async Task<bool> CheckCategoryTitleExisted(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var check = await _categoryRepository.CheckTitleExists(command.Title);
            return !check;
        }

    }
}
