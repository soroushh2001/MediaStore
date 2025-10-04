using MediaStore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaStore.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
    }
}
