using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class NutrientRepository : INutrientRepository
    {
        private readonly CookBoxContext _context;

        public NutrientRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteNutrient(int id)
        {
            var currentNutrient = await GetNutrient(id);
            _context.Nutrients.Remove(currentNutrient);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Nutrient> GetNutrient(int id)
        {
            var nutrient = await _context.Nutrients
                  .FirstOrDefaultAsync(x => x.Id == id);
            return nutrient;
        }

        public async Task<IEnumerable<Nutrient>> GetNutrients()
        {
            var nutrients = await _context.Nutrients
                  .ToListAsync();
            return nutrients;
        }

        public async Task InsertNutrient(Nutrient nutrient)
        {
            await _context.Nutrients.AddAsync(nutrient);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateNutrient(Nutrient nutrient)
        {
            _context.Nutrients.Update(nutrient);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
