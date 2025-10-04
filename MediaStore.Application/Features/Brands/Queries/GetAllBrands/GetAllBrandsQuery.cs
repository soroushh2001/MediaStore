using MediaStore.Application.Features.Brands.Shared;
using MediatR;

namespace MediaStore.Application.Features.Brands.Queries.GetAllBrands
{
    public record GetAllBrandsQuery(bool? IsDeleted) : IRequest<List<BrandResponse>>;
}
