using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface ISessionService
    {
        Task<SessionViewModel> GetSession(int id);
        Task InsertSession(SessionViewModel sessionViewModel);
        Task<bool> UpdateSession(SessionViewModel sessionViewModel);
        Task<bool> DeleteSession(int id);
        Task<PagedList<SessionViewModel>> GetSessions(SessionListSearch sessionListSearch);
    }
}
