using AutoMapper;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.Services
{
    public class MenuStoreService : IMenuStoreService
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
            var menuStore = await _menuStoreRepository.GetMenuStore(id);
            if (menuStore == null)
            {
                return false;
            }
            else
            {
                return await _menuStoreRepository.DeleteMenuStore(id);
            }
        }

        public async Task<PagedList<MenuStoreViewModel>> GetMenuStores(MenuStoreListSearch menuStoreListSearch)
        {
            var menuStores = await _menuStoreRepository.GetMenuStores();
            var count = menuStores.Count();

            var dataPage = menuStores
                        .Skip((menuStoreListSearch.page_number - 1) * menuStoreListSearch.page_size)
              .Take(menuStoreListSearch.page_size);

            var menuStoreViewModels = _mapper.Map<IEnumerable<MenuStoreViewModel>>(dataPage);

            return new PagedList<MenuStoreViewModel>(menuStoreViewModels.ToList(),
                count, menuStoreListSearch.page_number, menuStoreListSearch.page_size);
        }


        public async Task<MenuStoreViewModel> GetMenuStore(int id)
        {
            var menuStore = await _menuStoreRepository.GetMenuStore(id);

            if (menuStore == null)
            {
                return null;
            }
            var dishViewModel = _mapper.Map<MenuStoreViewModel>(menuStore);



            return dishViewModel;
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
