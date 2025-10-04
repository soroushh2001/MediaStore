using MediaStore.Application.Contracts.Persistence;
using MediaStore.Domain.Entities;
using MediaStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Persistence.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        #region Constructor

        private readonly MediaStoreDbContext _context;

        public BrandRepository(MediaStoreDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<List<Brand>> GetAllBrandsAsync(bool? isDeleted)
        {
            var query = _context.Brands.AsQueryable();
            if (isDeleted.HasValue)
            {
                query = query.Where(b => b.IsDeleted == isDeleted);
            }
            return await query.ToListAsync();
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            return await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateBrandAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }

        public void UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
        }

        public async Task<bool> CheckBrandTitleExisted(string title, int? id = null)
        {
            return await _context.Brands.AnyAsync(b => b.Title == title && b.Id != id);
        }

        public async Task<bool> CheckBrandSlugExisted(string slug, int? id = null)
        {
            return await _context.Brands.AnyAsync(b => b.Slug == slug && b.Id != id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
