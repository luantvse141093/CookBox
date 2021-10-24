using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.IRepositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetMenus();
        Task<Menu> GetMenu(int id);
        Task InsertMenu(Menu Menu);
        Task<bool> UpdateMenu(Menu Menu);
        Task<bool> DeleteMenu(int id);
    }
}
