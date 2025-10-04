using System.ComponentModel.DataAnnotations;

namespace MediaStore.Application.Features.Products.Queries.GetFilteredProducts
{
    public class ProductListItemResponse
    {
        public int Id { get; set; }
        public string Slug { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Price { get; set; }
        public string ImageName { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
