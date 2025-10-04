using MediaStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaStore.Persistence.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Title = "تلویزیون",
                    Slug = "tv"
                },
                new Category
                {
                    Id = 2,
                    Title = "FullHd",
                    Slug = "full-hd",
                    ParentId = 1,
                },
                new Category
                {
                    Id = 3,
                    Title = "4K",
                    Slug = "4k",
                    ParentId = 1,
                }
            };

            builder.HasData(categories);
        }
    }
}
