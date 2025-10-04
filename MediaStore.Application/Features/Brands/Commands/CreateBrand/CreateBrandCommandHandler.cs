using AutoMapper;
using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Application.StaticDetails;
using MediaStore.Domain.Entities;
using MediatR;

namespace MediaStore.Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, ApiResponse<bool>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }


        public async Task<ApiResponse<bool>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<bool>();
            var validator = new CreateBrandCommandValidator(_brandRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.StatusCode = StatusCodes.BadRequest;
                response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            request.Slug = request.Slug.GenerateSlug();

            var brand = _mapper.Map<Brand>(request);

            await _brandRepository.CreateBrandAsync(brand);
            await _brandRepository.SaveChangesAsync();
            return response;
        }
    }
}
