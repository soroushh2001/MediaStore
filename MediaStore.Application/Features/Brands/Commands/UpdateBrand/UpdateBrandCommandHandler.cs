using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Application.StaticDetails;
using MediatR;

namespace MediaStore.Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, ApiResponse<bool>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();
            var brandToUpdate = await _brandRepository.GetByIdAsync(request.Id);

            if (brandToUpdate == null)
            {
                response.StatusCode = StatusCodes.NotFound;
                response.IsSuccess = false;
                return response;
            }

            var validator = new UpdateBrandCommandValidator(_brandRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.StatusCode = StatusCodes.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }

            request.Slug = request.Slug.GenerateSlug();

            brandToUpdate = _mapper.Map(request, brandToUpdate);

            brandToUpdate.LastModifiedDate = DateTime.Now;

            _brandRepository.Update(brandToUpdate);
            await _brandRepository.SaveChangesAsync();
            return response;
        }
    }
}
