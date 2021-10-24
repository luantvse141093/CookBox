using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface IMenuDetailRepository
    {
        Task<IEnumerable<MenuDetail>> GetMenuDetails();
        Task<MenuDetail> GetMenuDetail(int id);
        Task InsertMenuDetail(MenuDetail menuDetail);
        Task<bool> UpdateMenuDetail(MenuDetail menuDetail);
        Task<bool> DeleteMenuDetail(int id);
    }
}
