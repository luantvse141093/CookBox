using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CookingBox.Data.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly CookBoxContext _context;

        public DishRepository(CookBoxContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteDish(int id)
        {
            var dish = await _context.Dishes
                .FirstOrDefaultAsync(x => x.Id == id);
            dish.Status = false;
            _context.Dishes.Update(dish);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Dish> GetDish(int id)
        {
            var dish = await _context.Dishes
                  .Include(x => x.Category)
                  .Include(x => x.TasteDetails).ThenInclude(y => y.Taste)
                  .Include(x => x.Repices).ThenInclude(y => y.Steps)
                  .Include(x => x.NutrientDetails).ThenInclude(y => y.Nutrient)
                  .Include(x => x.DishIngredients).ThenInclude(y => y.Metarial)
                  .FirstOrDefaultAsync(x => x.Id == id);
            return dish;
        }

        public async Task<IEnumerable<Dish>> GetDishes()
        {
            var dishes = await _context.Dishes
                  //.Include(x => x.TasteDetails).ThenInclude(y => y.Taste)
                  //.Include(x => x.Category)
                  //.Include(x => x.Repices).ThenInclude(y => y.Steps)
                  .ToListAsync();
            return dishes;
        }

        public async Task InsertDish(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDish(Dish dish)
        {
            _context.Dishes.Update(dish);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
