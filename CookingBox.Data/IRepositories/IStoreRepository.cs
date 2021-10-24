using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.IRepositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store> GetStore(int id);
        Task<int> InsertStore(Store Store);
        Task<bool> UpdateStore(Store Store);
        Task<bool> DeleteStore(int id);
    }
}
