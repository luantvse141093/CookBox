using CookingBox.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Data.IRepositories;


namespace CookingBox.Data.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CookBoxContext _context;
        public UsersRepository(CookBoxContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var posts = await _context.Users.Include(x => x.Role).ToListAsync();
            return posts;
        }
        public async Task<User> GetUser(int id)
        {
            var post = await _context.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }
        public async Task<int> InsertUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUser(id);
            user.Status = false;
            _context.Users.Update(user);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var post = await _context.Users
              .Include(x => x.Role)
              .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return post;

        }
    }
}
