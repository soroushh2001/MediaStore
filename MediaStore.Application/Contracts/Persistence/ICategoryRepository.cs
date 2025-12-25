using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Query();
        Task<Category?> GetByIdAsync(int id);
        Task CreateAsync(Category category);
        void Update(Category category);
        Task<bool> CheckTitleExists(string title, int? id = null);
        Task<bool> CheckSlugExisted(string slug, int? id = null);
        Task SaveChangesAsync();
    }
}
