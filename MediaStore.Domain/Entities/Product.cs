using MediaStore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MediaStore.Domain.Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(250)]
        public string Slug { get; set; } = null!;

        [MaxLength(250)]
        public string Title { get; set; } = null!;

        public int Price { get; set; } 

        public string? Description { get; set; }

        public string ImageName { get; set; } = null!;

        public int BrandId { get; set; }
        public Brand? Brand { get; set; } 

        public ICollection<ProductCategory>? ProductCategories { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
