using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class TasteRepository : ITasteRepository
    {
        private readonly CookBoxContext _context;

        public TasteRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTaste(int id)
        {
            var currentTaste = await GetTaste(id);
            _context.Tastes.Remove(currentTaste);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Taste> GetTaste(int id)
        {
            var taste = await _context.Tastes
                  .FirstOrDefaultAsync(x => x.Id == id);
            return taste;
        }

        public async Task<IEnumerable<Taste>> GetTastes()
        {
            var tastes = await _context.Tastes
                  .ToListAsync();
            return tastes;
        }

        public async Task InsertTaste(Taste taste)
        {
            await _context.Tastes.AddAsync(taste);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTaste(Taste taste)
        {
            _context.Tastes.Update(taste);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
