using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class MenuDetailRepository : IMenuDetailRepository
    {
        private readonly CookBoxContext _context;

        public MenuDetailRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMenuDetail(int id)
        {
            var currentMenuDetail = await GetMenuDetail(id);
            _context.MenuDetails.Remove(currentMenuDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<MenuDetail> GetMenuDetail(int id)
        {
            var menuDetail = await _context.MenuDetails
                  .FirstOrDefaultAsync(x => x.Id == id);
            return menuDetail;
        }

        public async Task<IEnumerable<MenuDetail>> GetMenuDetails()
        {
            var menuDetails = await _context.MenuDetails
                  .ToListAsync();
            return menuDetails;
        }

        public async Task InsertMenuDetail(MenuDetail menuDetail)
        {
            await _context.MenuDetails.AddAsync(menuDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateMenuDetail(MenuDetail menuDetail)
        {
            _context.MenuDetails.Update(menuDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
