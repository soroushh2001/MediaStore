using MediaStore.Application.Features.Brands.Shared;
using MediaStore.Application.Features.Categories.Shared;
using MediaStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace MediaStore.Application.Features.Products.Queries.GetProductById
{
    public class ProductResponse 
    {
        public string Slug { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int Price { get; set; }

        public string? Description { get; set; }

        public string ImageName { get; set; } = null!;
        public BrandResponse? Brand { get; set; }

        public List<CategoryResponse>? Categories { get; set; }
    }
}
