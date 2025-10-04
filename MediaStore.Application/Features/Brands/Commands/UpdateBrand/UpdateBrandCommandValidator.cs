using FluentValidation;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Brands.Commands.CreateBrand;
using MediaStore.Application.Features.Brands.Shared;

namespace MediaStore.Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        private readonly IBrandRepository _brandRepository;
        public UpdateBrandCommandValidator(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            Include(new BaseBrandCommandValidator());
            RuleFor(b => b)
             .MustAsync(CheckBrandSlugExisted)
             .WithMessage("این اسلاگ قبلا ثبت شده است.");
            RuleFor(b => b)
              .MustAsync(CheckBrandTitleExisted)
              .WithMessage("این عنوان قبلا ثبت شده است.");
        }

        private async Task<bool> CheckBrandSlugExisted(UpdateBrandCommand command, CancellationToken cancellationToken)
        {
            var check = await _brandRepository.CheckBrandSlugExisted(command.Slug, command.Id);
            return !check;
        }

        private async Task<bool> CheckBrandTitleExisted(UpdateBrandCommand command, CancellationToken cancellationToken)
        {
            var check = await _brandRepository.CheckBrandTitleExisted(command.Title, command.Id);
            return !check;
        }

    }
}
