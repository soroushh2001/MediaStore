using AutoMapper;
using MediaStore.Application.Features.Brands.Commands.CreateBrand;
using MediaStore.Application.Features.Brands.Commands.UpdateBrand;
using MediaStore.Application.Features.Brands.Shared;
using MediaStore.Domain.Entities;

namespace MediaStore.Application.MappingProfiles
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<CreateBrandCommand, Brand>();
            CreateMap<UpdateBrandCommand, Brand>().ReverseMap();
            CreateMap<Brand, BrandResponse>();
        }
    }
}
