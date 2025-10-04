using MediaStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaStore.Persistence.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            var brands = new List<Brand>
            {
                new Brand
                {
                    Id = 1,
                    Title = "سامسونگ",
                    Slug = "samsung",
                },
                new Brand
                {
                    Id = 2,
                    Title = "ال جی",
                    Slug = "lg"
                }
            };

            builder.HasData(brands);
        }
    }
}
