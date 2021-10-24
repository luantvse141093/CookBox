using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class MetarialRepository : IMetarialRepository
    {
        private readonly CookBoxContext _context;

        public MetarialRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMetarial(int id)
        {
            var currentMetarial = await GetMetarial(id);
            _context.Metarials.Remove(currentMetarial);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Metarial> GetMetarial(int id)
        {
            var metarial = await _context.Metarials
                  .FirstOrDefaultAsync(x => x.Id == id);
            return metarial;
        }

        public async Task<IEnumerable<Metarial>> GetMetarials()
        {
            var metarials = await _context.Metarials
                  .ToListAsync();
            return metarials;
        }

        public async Task InsertMetarial(Metarial metarial)
        {
            await _context.Metarials.AddAsync(metarial);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateMetarial(Metarial metarial)
        {
            _context.Metarials.Update(metarial);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
