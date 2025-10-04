using FluentValidation;

namespace MediaStore.Application.Features.Categories.Shared
{
    public class BaseCategoryValidator : AbstractValidator<BaseCategoryCommand>
    {
        public BaseCategoryValidator() 
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
