using MediaStore.Application.Common.Responses;
using MediaStore.Application.Specifications;
using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IProductRepository 
    {
        Task<PaginatedResponse<Product>> GetFilteredProductsAsync(FilterProductSpecification specification);    
        Task CreateProductAsync(Product product);
        void UpdateProduct(Product product);
        Task<Product?> GetProductByIdAsync(int id);
        Task AddCategoriesToProductAsync(List<ProductCategory> categories);
        Task RemoveCategoriesFromProductAsync(int productId);
        Task SaveChangesAsync();
    }
}
