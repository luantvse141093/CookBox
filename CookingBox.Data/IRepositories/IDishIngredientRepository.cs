using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface IDishIngredientRepository
    {
        Task<IEnumerable<DishIngredient>> GetDishIngredients();
        Task<DishIngredient> GetDishIngredient(int id);
        Task InsertDishIngredient(DishIngredient dishIngredient);
        Task<bool> UpdateDishIngredient(DishIngredient dishIngredient);
        Task<bool> DeleteDishIngredient(int id);

    }
}
