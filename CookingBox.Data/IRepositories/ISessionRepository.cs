using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetSessions();
        Task<Session> GetSession(int id);
        Task InsertSession(Session session);
        Task<bool> UpdateSession(Session session);
        Task<bool> DeleteSession(int id);
    }
}
