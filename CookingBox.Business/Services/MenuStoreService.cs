using System;
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
    public class MenuStoreService: IMenuStoreService
    {
        private readonly IMenuStoreRepository _menuStoreRepository;
        private readonly IMapper _mapper;
        public MenuStoreService(IMenuStoreRepository menuStoreRepository, IMapper mapper)
        {
            _menuStoreRepository = menuStoreRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteMenuStore(int id)
        {
            var menuStore = await _menuStoreRepository.GetMenusStore(id);
            if (menuStore == null)
            {
                return false;
            }
            else
            {
                return await _menuStoreRepository.DeleteMenuStore(id);
            }
        }

        public Task<PagedList<MenuStoreViewModel>> GetMenus(MenuStoreListSearch menuStoreListSearch)
        {
            throw new NotImplementedException();
        }

        public Task<MenuStoreViewModel> GetMenuStore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertMenuStore(MenuStoreViewModel menuStoreViewModel)
        {
            var menuStore = _mapper.Map<MenuStore>(menuStoreViewModel);
            return await _menuStoreRepository.InsertMenuStore(menuStore);
        }

        public async Task<bool> UpdateMenuStore(MenuStoreViewModel menuStoreViewModel)
        {
            var menuStore = _mapper.Map<MenuStore>(menuStoreViewModel);
            return await _menuStoreRepository.UpdateMenuStore(menuStore);
        }
    }
}
