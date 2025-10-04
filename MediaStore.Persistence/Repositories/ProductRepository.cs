using MediaStore.Application.Common.Responses;
using MediaStore.Application.Contracts.Persistence;
using MediaStore.Application.Extensions;
using MediaStore.Application.Specifications;
using MediaStore.Domain.Entities;
using MediaStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MediaStoreDbContext _context;
        public ProductRepository(MediaStoreDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResponse<Product>> GetFilteredProductsAsync(FilterProductSpecification specification)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductCategories)!
                .ThenInclude(pc => pc.Category)
                .AsQueryable();

            if (specification.BrandSlugs != null)
            {
                query = query.Where(p=>specification.BrandSlugs.Contains(p.Brand.Slug));
            }

            if(!string.IsNullOrEmpty(specification.CategorySlug))
            {
                query = query.Where(p => p.ProductCategories != null &&
                p.ProductCategories.Any(p => p.Category.Slug == specification.CategorySlug));
            }

            if (!string.IsNullOrEmpty(specification.Search))
            {
                specification.SortBy = "Title";
                query = query.Where(p => p.Title.Contains(specification.Search))
                    .OrderByDescending(p => p.Title.StartsWith(specification.Search) ? 1 : 0);
            }

            else
            {
                if (!string.IsNullOrEmpty(specification.SortBy))
                {
                    var orderCondition = specification.OrderBy == "Desc";


                    switch (specification.SortBy)
                    {
                        case "ModifiedDate":
                            query = orderCondition
                                ? query.OrderByDescending(x => x.LastModifiedDate)
                                : query.OrderBy(x => x.LastModifiedDate);
                            break;
                        case "Price":
                            query = orderCondition ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price);
                            break;
                        case "Title":
                            query = orderCondition ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title);
                            break;
                    }
                }
            }

            var paging = await PaginatedList<Product>.CreateAsync(query, specification.PageIndex, specification.PageSize, specification.PageRange);

            return paging;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddCategoriesToProductAsync(List<ProductCategory> categories)
        {
            await _context.ProductCategories.AddRangeAsync(categories);
        }

        public async Task RemoveCategoriesFromProductAsync(int productId)
        {
            _context.ProductCategories.RemoveRange(await _context.ProductCategories.Where(x => x.ProductId == productId).ToListAsync());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
