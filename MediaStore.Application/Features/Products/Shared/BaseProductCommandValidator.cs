using FluentValidation;

namespace MediaStore.Application.Features.Products.Shared
{
    public class BaseProductCommandValidator : AbstractValidator<BaseProductCommand>
    {
        public BaseProductCommandValidator()
        {
            RuleFor(p => p.Title).NotNull().
               NotEmpty()
               .WithMessage("لطفا {PropertyName} را وارد کنید.")
               .MaximumLength(250)
               .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر داشته باشد")
               .WithName("عنوان");
            RuleFor(p => p.Price).NotNull().
              NotEmpty()
              .WithMessage("لطفا {PropertyName} را وارد کنید.")
              .WithName("قیمت");

            RuleFor(p => p.BrandId).NotNull().
             NotEmpty()
             .WithMessage("لطفا {PropertyName} را وارد کنید.")
             .WithName("برند");

            RuleFor(p => p.CategoryIds).NotNull().
            NotEmpty()
            .WithMessage("لطفا {PropertyName} را وارد کنید.")
            .WithName("دسته بندی");
        }
    }
}
