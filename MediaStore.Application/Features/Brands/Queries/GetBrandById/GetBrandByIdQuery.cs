using MediaStore.Application.Common.Responses;
using MediaStore.Application.Features.Brands.Shared;
using MediaStore.Domain.Entities;
using MediatR;

namespace MediaStore.Application.Features.Brands.Queries.GetBrandById
{
    public record GetBrandByIdQuery(int Id) : IRequest<ApiResponse<BrandResponse>>;
}
