using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CookBoxContext _context;

        public CategoryRepository(CookBoxContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteCategory(int id)
        {
            var currentPost = await GetCategory(id);
            currentPost.Status = false;
            _context.Categories.Update(currentPost);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var posts = await _context.Categories
                .Include(x => x.Dishes)
                .ToListAsync();
            return posts;
        }

        public async Task<Category> GetCategory(int id)
        {
            var post = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task InsertCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
