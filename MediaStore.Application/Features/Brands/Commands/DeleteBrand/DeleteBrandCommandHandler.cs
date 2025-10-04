using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.StaticDetails;
using MediatR;

namespace MediaStore.Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, ApiResponse<bool>>
    {
        private readonly IBrandRepository _brandRepository;
        public DeleteBrandCommandHandler(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();

            var brand = await _brandRepository.GetBrandByIdAsync(request.Id);
            
            if (brand == null)
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.NotFound;
                return response;
            }

            brand.IsDeleted = true;
            _brandRepository.UpdateBrand(brand);
            await _brandRepository.SaveChangesAsync();
            return response;

        }
    }
}
