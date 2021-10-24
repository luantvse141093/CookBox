using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly CookBoxContext _context;

        public StoreRepository(CookBoxContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteStore(int id)
        {
            var currentPost = await GetStore(id);
            currentPost.Status = false;
            _context.Stores.Update(currentPost);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Store> GetStore(int id)
        {
            var post = await _context.Stores
              .FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            var posts = await _context.Stores.ToListAsync();
            return posts;
        }

        public async Task<int> InsertStore(Store store)
        {
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();
            return store.Id;
        }

        public async Task<bool> UpdateStore(Store store)
        {
            _context.Stores.Update(store);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
