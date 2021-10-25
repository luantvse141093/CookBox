using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly CookBoxContext _context;

        public MenuRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMenu(int id)
        {
            var currentMenu = await GetMenu(id);
            _context.Menus.Remove(currentMenu);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Menu> GetMenu(int id)
        {
            var menu = await _context.Menus
                  .Include(x => x.Session)
                  .Include(x => x.MenuStores)
                  .Include(x => x.MenuDetails).ThenInclude(y => y.Dish)
                  .FirstOrDefaultAsync(x => x.Id == id);
            return menu;
        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {
            var menus = await _context.Menus
                  .Include(x => x.Session)
                  .Include(x => x.MenuStores)
                  .Include(x => x.MenuDetails).ThenInclude(y => y.Dish)
                  .ToListAsync();
            return menus;
        }

        public async Task InsertMenu(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            foreach (var item in menu.MenuDetails)
            {
                _context.Entry(item.Dish).State = EntityState.Unchanged;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateMenu(Menu menu)
        {
            _context.Menus.Update(menu);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
