using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class RepiceRepository : IRepiceRepository
    {
        private readonly CookBoxContext _context;

        public RepiceRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteRepice(int id)
        {
            var currentRepice = await GetRepice(id);
            _context.Repices.Remove(currentRepice);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Repice> GetRepice(int id)
        {
            var repice = await _context.Repices
                  .FirstOrDefaultAsync(x => x.Id == id);
            return repice;
        }

        public async Task<IEnumerable<Repice>> GetRepices()
        {
            var repices = await _context.Repices
                  .ToListAsync();
            return repices;
        }

        public async Task InsertRepice(Repice repice)
        {
            await _context.Repices.AddAsync(repice);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateRepice(Repice repice)
        {
            _context.Repices.Update(repice);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
