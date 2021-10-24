using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class NutrientDetailRepository : INutrientDetailRepository
    {
        private readonly CookBoxContext _context;

        public NutrientDetailRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteNutrientDetail(int id)
        {
            var currentNutrientDetail = await GetNutrientDetail(id);
            _context.NutrientDetails.Remove(currentNutrientDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<NutrientDetail> GetNutrientDetail(int id)
        {
            var nutrientDetail = await _context.NutrientDetails
                  .FirstOrDefaultAsync(x => x.Id == id);
            return nutrientDetail;
        }

        public async Task<IEnumerable<NutrientDetail>> GetNutrientDetails()
        {
            var nutrientDetails = await _context.NutrientDetails
                  .ToListAsync();
            return nutrientDetails;
        }

        public async Task InsertNutrientDetail(NutrientDetail nutrientDetail)
        {
            await _context.NutrientDetails.AddAsync(nutrientDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateNutrientDetail(NutrientDetail nutrientDetail)
        {
            _context.NutrientDetails.Update(nutrientDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
