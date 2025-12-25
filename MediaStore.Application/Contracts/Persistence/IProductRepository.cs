using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IProductRepository 
    {
        IQueryable<Product> GetQuryable();    
        Task CreateProductAsync(Product product);
        void UpdateProduct(Product product);
        Task<Product?> GetProductByIdAsync(int id);
        Task AddCategoriesToProductAsync(List<ProductCategory> categories);
        Task RemoveCategoriesFromProductAsync(int productId);
        Task SaveChangesAsync();
    }
}
