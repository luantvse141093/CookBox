using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.ModelSearch.User;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.IServices
{
    public interface IMenuService
    {
        Task<MenuViewModel> GetMenu(int id);
        Task InsertMenu(MenuViewModel menuViewModel);
        Task<bool> UpdateMenu(MenuViewModel menuViewModel);
        Task<bool> DeleteMenu(int id);
        Task<PagedList<MenuViewModel>> GetMenus(MenuListSearch menuListSearch);
    }
}
