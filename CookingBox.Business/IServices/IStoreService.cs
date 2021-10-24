using CookingBox.Business.CustomEntities.ModelSearch;
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
    public interface IStoreService
    {
        Task<StoreViewModel> GetStore(int id);
        Task<int> InsertStore(StoreViewModel storeViewModel);
        Task<bool> UpdateStore(StoreViewModel storeViewModel);
        Task<bool> DeleteStore(int id);
        Task<PagedList<StoreViewModel>> GetStores(StoreListSearch storeListSearch);
    }
}
