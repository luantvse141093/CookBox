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
                  .Include(x => x.MenuStores).ThenInclude(y=>y.Store)
                  .Include(x => x.MenuDetails).ThenInclude(y => y.Dish)
                  .FirstOrDefaultAsync(x => x.Id == id);
            return menu;
        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {
            var menus = await _context.Menus
                  .Include(x => x.Session)
                  .Include(x => x.MenuStores).ThenInclude(y => y.Store)
                 .Include(x => x.MenuDetails).ThenInclude(y => y.Dish)
                  .ToListAsync();
            return menus;
        }

        public async Task<int> InsertMenu(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
           // _context.Entry(menu.Session).State = EntityState.Unchanged;
            foreach (var item in menu.MenuDetails)
            {
                _context.Entry(item.Dish).State = EntityState.Unchanged;
            }
            await _context.SaveChangesAsync();
            return menu.Id;
        }

        public async Task<bool> UpdateMenu(Menu menu)
        {
            //var menuU = await GetMenu(menu.Id);
            //menu.
            _context.Menus.Update(menu);
           // _context.Entry(menu.MenuDetails).State = EntityState.Modified;
            foreach (var item in menu.MenuDetails)
            {
                _context.Entry(item.Dish).State = EntityState.Unchanged;
            }
     
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
