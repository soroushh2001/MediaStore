﻿using MediaStore.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaStore.Domain.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [MaxLength(250)]
        public string Slug { get; set; } = null!;

        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ICollection<Category>? Categories { get; set; }

    }
}
