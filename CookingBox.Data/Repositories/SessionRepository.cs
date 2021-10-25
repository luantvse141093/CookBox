using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CookingBox.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly CookBoxContext _context;

        public SessionRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSession(int id)
        {
            var currentSession = await GetSession(id);
            _context.Sessions.Remove(currentSession);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Session> GetSession(int id)
        {
            var session = await _context.Sessions
                  .FirstOrDefaultAsync(x => x.Id == id);
            return session;
        }

        public async Task<IEnumerable<Session>> GetSessions()
        {
            var sessions = await _context.Sessions
                  .ToListAsync();
            return sessions;
        }

        public async Task InsertSession(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSession(Session session)
        {
            _context.Sessions.Update(session);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
