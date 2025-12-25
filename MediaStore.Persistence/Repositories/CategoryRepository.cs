using MediaStore.Application.Contracts.Persistence;
using MediaStore.Domain.Entities;
using MediaStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaStore.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MediaStoreDbContext _context;
        public CategoryRepository(MediaStoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Query()
        {
            return _context.Categories.AsQueryable();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task<bool> CheckTitleExists(string title, int? id = null)
        {
            return await _context.Categories.AnyAsync(c => c.Title == title && c.Id != id);
        }

        public async Task<bool> CheckSlugExisted(string slug, int? id = null)
        {
            return await _context.Categories.AnyAsync(c => c.Slug == slug && c.Id != id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
