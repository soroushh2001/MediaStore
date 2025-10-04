using AutoMapper;
using CarPartsShop.Application.Extensions;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Application.StaticDetails;
using MediaStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,ApiResponse<bool>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();

            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.BadRequest;
                response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var product = _mapper.Map<Product>(request);
            product.Slug = request.Title.GenerateSlug();


            var imageName = request.Image.FileNameGenerator();
            var upResult = await request.Image.UploadImage(imageName, ImagesPath.ProductImageOrgServerPath, ImagesPath.ProductImageThumbServerPath);
            if (!upResult)
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.BadRequest;
                response.ErrorMessages.Add("فرمت عکس وارد شده صحیح نمی باشد");
                return response;
            }

            await _productRepository.CreateProductAsync(product);
            await _productRepository.SaveChangesAsync();
            var categories = request.CategoryIds.Select(x => new ProductCategory
            {
                ProductId = product.Id,
                CategoryId = x
            }).ToList();

            await _productRepository.AddCategoriesToProductAsync(categories);
            await _productRepository.SaveChangesAsync();
            return response;
        }
    }
}
