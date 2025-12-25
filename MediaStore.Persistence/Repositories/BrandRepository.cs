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
        
        public IQueryable<Brand> Query()
        {
            return _context.Brands.AsQueryable();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
        }

        public async Task<bool> CheckTitleExists(string title, int? id = null)
        {
            return await _context.Brands.AnyAsync(b => b.Title == title && b.Id != id);
        }

        public async Task<bool> CheckSlugExists(string slug, int? id = null)
        {
            return await _context.Brands.AnyAsync(b => b.Slug == slug && b.Id != id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
