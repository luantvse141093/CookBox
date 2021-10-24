using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class DishIngredientRepository : IDishIngredientRepository
    {
        private readonly CookBoxContext _context;

        public DishIngredientRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDishIngredient(int id)
        {
            var currentDishIngredient = await GetDishIngredient(id);
            _context.DishIngredients.Remove(currentDishIngredient);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<DishIngredient> GetDishIngredient(int id)
        {
            var dishIngredient = await _context.DishIngredients
                  .FirstOrDefaultAsync(x => x.Id == id);
            return dishIngredient;
        }

        public async Task<IEnumerable<DishIngredient>> GetDishIngredients()
        {
            var dishIngredients = await _context.DishIngredients
                  .ToListAsync();
            return dishIngredients;
        }

        public async Task InsertDishIngredient(DishIngredient dishIngredient)
        {
            await _context.DishIngredients.AddAsync(dishIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDishIngredient(DishIngredient dishIngredient)
        {
            _context.DishIngredients.Update(dishIngredient);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
