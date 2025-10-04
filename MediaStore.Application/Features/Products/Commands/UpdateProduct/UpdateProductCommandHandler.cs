using AutoMapper;
using CarPartsShop.Application.Extensions;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Application.StaticDetails;
using MediaStore.Domain.Entities;
using MediatR;
using SixLabors.ImageSharp;

namespace MediaStore.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<bool>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();

            var productToUpdate = await _productRepository.GetProductByIdAsync(request.Id);

            if (productToUpdate == null)
            {
                response.StatusCode = StatusCodes.NotFound;
                response.IsSuccess = false;
                return response;

            }

            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.StatusCode = StatusCodes.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }

            productToUpdate.Slug = request.Title.GenerateSlug();

            if (request.Image != null && request.Image.Length > 0)
            {
                var imageName = request.Image.FileNameGenerator();
                productToUpdate.ImageName.DeleteImage(ImagesPath.ProductImageOrgServerPath, ImagesPath.ProductImageThumbServerPath);
                var upResult = await request.Image.UploadImage(imageName, ImagesPath.ProductImageOrgServerPath, ImagesPath.ProductImageThumbServerPath);
                if (!upResult)
                {
                    response.IsSuccess = false;
                    response.StatusCode = StatusCodes.BadRequest;
                    response.ErrorMessages.Add("فرمت عکس وارد شده صحیح نمی باشد");
                    return response;
                }
                productToUpdate.ImageName = imageName;
            }

            productToUpdate = _mapper.Map(request, productToUpdate);
            _productRepository.UpdateProduct(productToUpdate);
            await _productRepository.SaveChangesAsync();

            await _productRepository.RemoveCategoriesFromProductAsync(request.Id);
            await _productRepository.SaveChangesAsync();

            await _productRepository.AddCategoriesToProductAsync(request.CategoryIds.Select(x => new ProductCategory
            {
                ProductId = request.Id,
                CategoryId = x
            }).ToList());

            await _productRepository.SaveChangesAsync();
            return response;
        }
    }
}
