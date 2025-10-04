using FluentValidation;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Features.Brands.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        private readonly IBrandRepository _brandRepository;
        public CreateBrandCommandValidator(IBrandRepository brandRepository) 
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

        private async Task<bool> CheckBrandSlugExisted(CreateBrandCommand command, CancellationToken cancellationToken)
        {
            var check = await _brandRepository.CheckBrandSlugExisted(command.Slug);
            return !check;
        }

        private async Task<bool> CheckBrandTitleExisted(CreateBrandCommand command, CancellationToken cancellationToken)
        {
            var check = await _brandRepository.CheckBrandTitleExisted(command.Title);
            return !check;
        }
    }
}
