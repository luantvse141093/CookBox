using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class TasteDetailRepository : ITasteDetailRepository
    {
        private readonly CookBoxContext _context;

        public TasteDetailRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTasteDetail(int id)
        {
            var currentTasteDetail = await GetTasteDetail(id);
            _context.TasteDetails.Remove(currentTasteDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<TasteDetail> GetTasteDetail(int id)
        {
            var tasteDetail = await _context.TasteDetails
                  .FirstOrDefaultAsync(x => x.Id == id);
            return tasteDetail;
        }

        public async Task<IEnumerable<TasteDetail>> GetTasteDetails()
        {
            var tasteDetails = await _context.TasteDetails
                  .ToListAsync();
            return tasteDetails;
        }

        public async Task InsertTasteDetail(TasteDetail tasteDetail)
        {
            await _context.TasteDetails.AddAsync(tasteDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTasteDetail(TasteDetail tasteDetail)
        {
            _context.TasteDetails.Update(tasteDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
