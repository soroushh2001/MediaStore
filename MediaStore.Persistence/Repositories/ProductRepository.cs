using MediaStore.Application.Contracts.Persistence;
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

        public IQueryable<Product> GetQuryable()
        {
            return _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductCategories)!
                .ThenInclude(pc => pc.Category)
                .AsQueryable();
        }
    }
}
