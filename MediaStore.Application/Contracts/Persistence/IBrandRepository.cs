using MediaStore.Domain.Entities;

namespace MediaStore.Application.Contracts.Persistence
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAllBrandsAsync(bool? isDeleted);
        Task<Brand?> GetBrandByIdAsync(int id);
        Task CreateBrandAsync(Brand brand);
        void UpdateBrand(Brand brand);
        Task<bool> CheckBrandTitleExisted(string title, int? id = null);
        Task<bool> CheckBrandSlugExisted(string slug, int? id = null);
        Task SaveChangesAsync();
    }
}
