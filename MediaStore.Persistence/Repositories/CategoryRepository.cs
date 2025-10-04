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

        public async Task<List<Category>> GetAllCategoriesAsync(bool? isDeleted)
        {
            var query = _context.Categories.AsQueryable();

            if (isDeleted.HasValue)
            {
                query = query.Where(c => c.IsDeleted == isDeleted);
            }

            return await query.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task<bool> CheckCategoryTitleExisted(string title, int? id = null)
        {
            return await _context.Categories.AnyAsync(c => c.Title == title && c.Id != id);
        }

        public async Task<bool> CheckCategorySlugExisted(string slug, int? id = null)
        {
            return await _context.Categories.AnyAsync(c => c.Slug == slug && c.Id != id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
