using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface IMenuDetailService
    {
        Task<MenuDetailViewModel> GetMenuDetail(int id);
        Task InsertMenuDetail(MenuDetailViewModel menuDetailViewModel);
        Task<bool> UpdateMenuDetail(MenuDetailViewModel menuDetailViewModel);
        Task<bool> DeleteMenuDetail(int id);
        Task<PagedList<MenuDetailViewModel>> GetMenuDetails(MenuDetailListSearch menuDetailListSearch);
    }
}
