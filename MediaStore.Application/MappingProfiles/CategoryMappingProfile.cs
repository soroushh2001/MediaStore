using AutoMapper;
using MediaStore.Application.Features.Brands.Commands.UpdateBrand;
using MediaStore.Application.Features.Categories.Commands.CreateCategory;
using MediaStore.Application.Features.Categories.Commands.UpdateCategory;
using MediaStore.Application.Features.Categories.Shared;
using MediaStore.Domain.Entities;

namespace MediaStore.Application.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();
        }
    }
}
