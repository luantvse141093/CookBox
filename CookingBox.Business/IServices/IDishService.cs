using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.ModelSearch.User;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;
using CookingBox.Business.ViewModels.User;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CookingBox.Business.IServices
{
    public interface IDishService
    {
        Task<DishViewModel> GetDish(int id);
        Task InsertDish(DishViewModel DishViewModel);
        Task<bool> UpdateDish(DishViewModel dishViewModel);
        Task<bool> DeleteDish(int id);
        Task<PagedList<DishViewModel>> GetDishes(DishListSearch dishListSearch);

        Task<DishUserViewModel> GetDishUser(UserMenuListSearch userMenuListSearch);
        Task<PagedList<MenuDetail>> GetDishesUser(UserMenuListSearch userMenuListSearch);

        Task<DishUserViewModel> GetDishByTaste(UserMenuListSearch userMenuListSearch);



    }
}
