using MediaStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStore.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }

        public int Sum { get; set; }

        public bool IsFinally { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
