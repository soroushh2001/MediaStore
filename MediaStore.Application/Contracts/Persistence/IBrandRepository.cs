using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IBrandRepository
    {
        IQueryable<Brand> Query();
        Task<Brand?> GetByIdAsync(int id);
        Task CreateAsync(Brand brand);
        void Update(Brand brand);
        Task<bool> CheckTitleExists(string title, int? id = null);
        Task<bool> CheckSlugExists(string slug, int? id = null);
        Task SaveChangesAsync();
    }
}
