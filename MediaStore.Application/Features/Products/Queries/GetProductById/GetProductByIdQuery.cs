using MediaStore.Application.Common.Responses;
using MediatR;

namespace MediaStore.Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<ApiResponse<ProductResponse>>;
}
