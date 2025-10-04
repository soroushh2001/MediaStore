using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync(bool? isDeleted);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(Category category);
        void UpdateCategory(Category category);
        Task<bool> CheckCategoryTitleExisted(string title, int? id = null);
        Task<bool> CheckCategorySlugExisted(string slug, int? id = null);
        Task SaveChangesAsync();
    }
}
