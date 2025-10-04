using AutoMapper;
using MediaStore.Application.Features.Products.Commands.CreateProduct;
using MediaStore.Application.Features.Products.Commands.UpdateProduct;
using MediaStore.Application.Features.Products.Queries.GetFilteredProducts;
using MediaStore.Application.Features.Products.Queries.GetProductById;
using MediaStore.Domain.Entities;

namespace MediaStore.Application.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            CreateMap<Product, ProductListItemResponse>();
            CreateMap<Product, ProductResponse>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
        }
    }
}
