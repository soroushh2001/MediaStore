using MediaStore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaStore.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }
    }
}
