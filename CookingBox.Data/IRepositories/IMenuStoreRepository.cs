using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface IMenuStoreRepository
    {
        Task<IEnumerable<MenuStore>> GetMenuStores();
        Task<MenuStore> GetMenusStore(int id);
        Task<int> InsertMenuStore(MenuStore menuStore);
        Task<bool> UpdateMenuStore(MenuStore menuStore);
        Task<bool> DeleteMenuStore(int id);
    }
}
