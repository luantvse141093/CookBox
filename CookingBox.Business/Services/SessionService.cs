using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Business.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;
        public SessionService(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteSession(int id)
        {
            var categoryCheck = await _sessionRepository.GetSession(id);
            if (categoryCheck == null)
            {
                return false;
            }
            else
            {
                return await _sessionRepository.DeleteSession(id);
            }
        }

        public async Task<SessionViewModel> GetSession(int id)
        {
            var session = await _sessionRepository.GetSession(id);
            var sessionViewModel = _mapper.Map<SessionViewModel>(session);
            return sessionViewModel;
            
        }

        public async Task<PagedList<SessionViewModel>> GetSessions(SessionListSearch sessionListSearch)
        {
            var sessions = await _sessionRepository.GetSessions();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = sessions.Count();

            var dataPage = sessions
                        .Skip((sessionListSearch.page_number - 1) * sessionListSearch.page_size)
              .Take(sessionListSearch.page_size);

            var sessionViewModels = _mapper.Map<IEnumerable<SessionViewModel>>(dataPage);

            return new PagedList<SessionViewModel>(sessionViewModels.ToList(),
                count, sessionListSearch.page_number, sessionListSearch.page_size);
        }

        public async Task InsertSession(SessionViewModel sessionViewModel)
        {
            var session = _mapper.Map<Session>(sessionViewModel);
            await _sessionRepository.InsertSession(session);
        }

        public async Task<bool> UpdateSession(SessionViewModel sessionViewModel)
        {
            var session = _mapper.Map<Session>(sessionViewModel);
            return await _sessionRepository.UpdateSession(session);
        }
    }
}
