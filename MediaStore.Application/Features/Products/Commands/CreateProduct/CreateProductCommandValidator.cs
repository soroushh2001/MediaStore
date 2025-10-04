using FluentValidation;
using MediaStore.Application.Features.Brands.Shared;
using MediaStore.Application.Features.Products.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            Include(new BaseProductCommandValidator());
            RuleFor(p => p.Image).NotNull().
            NotEmpty()
            .WithMessage("لطفا {PropertyName} را وارد کنید.")
            .WithName("دسته بندی");
        }
    }
}
