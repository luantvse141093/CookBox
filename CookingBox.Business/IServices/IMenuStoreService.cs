using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface IMenuStoreService
    {
        Task<MenuStoreViewModel> GetMenuStore(int id);
        Task<int> InsertMenuStore(MenuStoreViewModel menuStoreViewModel);
        Task<bool> UpdateMenuStore(MenuStoreViewModel menuStoreViewModel);
        Task<bool> DeleteMenuStore(int id);
        Task<PagedList<MenuStoreViewModel>> GetMenuStores(MenuStoreListSearch menuStoreListSearch);
    }
}
