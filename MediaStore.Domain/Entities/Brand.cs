using MediaStore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MediaStore.Domain.Entities
{
    public class Brand : BaseEntity
    {
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [MaxLength(250)]
        public string Slug { get; set; } = null!;
    }
}
