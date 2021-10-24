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
    public class RoleRepository : IRoleRepository
    {
        private readonly CookBoxContext _context;

        public RoleRepository(CookBoxContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteRole(string id)
        {
            var role = await GetRole(id);
            if (role.Users.Count > 0)
            {
                return false;
            }
            role.Status = false;
            _context.Roles.Update(role);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Role> GetRole(string id)
        {
            var post = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return post;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            var posts = await _context.Roles
                .Include(x => x.Users)
                .ToListAsync();
            return posts;
        }

        public async Task InsertRole(Role role)
        {

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
