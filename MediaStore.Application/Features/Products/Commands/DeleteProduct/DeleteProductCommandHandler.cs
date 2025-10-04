using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.StaticDetails;
using MediatR;

namespace MediaStore.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand,ApiResponse<bool>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.NotFound;
                return response;
            }

            product.IsDeleted = true;

            _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();
            return response;
        }
    }
}
