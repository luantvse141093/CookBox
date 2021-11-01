using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CookingBox.Data.Repositories
{
    public class MenuStoreRepository : IMenuStoreRepository

    {
        private readonly CookBoxContext _context;

        public MenuStoreRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMenuStore(int id)
        {
            var menuStore = await _context.MenuStores
                .FirstOrDefaultAsync(x => x.Id == id);
            menuStore.Status = false;
            _context.MenuStores.Update(menuStore);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<MenuStore> GetMenuStore(int id)
        {
            var menuStore = await _context.MenuStores
                  .Include(x => x.Store)
                  .Include(x => x.Menu)
                  .Include(x => x.Session)
                  .FirstOrDefaultAsync(x => x.Id == id);
            return menuStore;
        }

        public async Task<IEnumerable<MenuStore>> GetMenuStores()
        {
            var menuStores = await _context.MenuStores
                .Include(x => x.Store)
                .Include(x => x.Menu)
                .Include(x => x.Session)
                .ToListAsync();
            return menuStores;
        }

        public async Task<int> InsertMenuStore(MenuStore menuStore)
        {

            await _context.MenuStores.AddAsync(menuStore);
            _context.Entry(menuStore.Menu).State = EntityState.Unchanged;
            _context.Entry(menuStore.Store).State = EntityState.Unchanged;
            _context.Entry(menuStore.Session).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
            return menuStore.Id;

        }

        public async Task<bool> UpdateMenuStore(MenuStore menuStore)
        {
            _context.MenuStores.Update(menuStore);
            _context.Entry(menuStore.Menu).State = EntityState.Unchanged;
            _context.Entry(menuStore.Store).State = EntityState.Unchanged;
            _context.Entry(menuStore.Session).State = EntityState.Unchanged;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
