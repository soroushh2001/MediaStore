using FluentValidation;
using MediaStore.Application.Contracts.Persistence;

namespace MediaStore.Application.Features.Brands.Shared
{
    public class BaseBrandCommandValidator : AbstractValidator<BaseBrandCommand>
    {
        public BaseBrandCommandValidator()
        {
            RuleFor(b => b.Title).NotNull().
                NotEmpty()
                .WithMessage("لطفا {PropertyName} را وارد کنید.")
                .MaximumLength(250)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر داشته باشد")
                .WithName("عنوان");

            RuleFor(b => b.Slug).NotNull().
                 NotEmpty()
                 .WithMessage("لطفا {PropertyName} را وارد کنید.")
                 .MaximumLength(250)
                 .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر داشته باشد")
                 .WithName("اسلاگ");
        }
    }
}
