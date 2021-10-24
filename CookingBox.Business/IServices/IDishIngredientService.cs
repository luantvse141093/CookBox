using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface IDishIngredientService
    {
        Task<DishIngredientViewModel> GetDishIngredient(int id);
        Task InsertDishIngredient(DishIngredientViewModel DishIngredientViewModel);
        Task<bool> UpdateDishIngredient(DishIngredientViewModel DishIngredientViewModel);
        Task<bool> DeleteDishIngredient(int id);
        Task<PagedList<DishIngredientViewModel>> GetDishIngredients(DishIngredientListSearch dishIngredientListSearch);
    }
}
