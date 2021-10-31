using AutoMapper;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.ModelSearch.User;
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
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public MenuService(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteMenu(int id)
        {
            var MenuCheck = await _menuRepository.GetMenu(id);
            if (MenuCheck == null)
            {
                throw new Exception("Menu doesn't exits");
            }
            else
            {
                return await _menuRepository.DeleteMenu(id);
            }
        }

        public async Task<MenuViewModel> GetMenu(int id)
        {
            var menu = await _menuRepository.GetMenu(id);
            var menuViewModel = _mapper.Map<MenuViewModel>(menu);
            menuViewModel.menu_details = _mapper.Map<ICollection<MenuDetailViewModel>>(menu.MenuDetails);
            menuViewModel.menu_stores = _mapper.Map<ICollection<MenuStoreViewModel>>(menu.MenuStores);

            return menuViewModel;
        }

        public async Task<PagedList<MenuViewModel>> GetMenus(MenuListSearch menuListSearch)
        {
            var menus = await _menuRepository.GetMenus();
            if (menuListSearch.store_id > 0)
            {
                menus = menus.Where(x => x.MenuStores.Any(y => y.StoreId == menuListSearch.store_id));
            }
            if (!string.IsNullOrEmpty(menuListSearch.name))
            {
                menus = menus.Where(x => x.Name.ToLower().Contains(menuListSearch.name.ToLower()));
            }
            if (menuListSearch.status.HasValue)
            {
                menus = menus.Where(x => x.Status == menuListSearch.status);
            }
            var count = menus.Count();
            var dataPage = menus
                        .Skip((menuListSearch.page_number - 1) * menuListSearch.page_size)
              .Take(menuListSearch.page_size);

            var menuViewModels = _mapper.Map<IEnumerable<MenuViewModel>>(dataPage);

            return new PagedList<MenuViewModel>(menuViewModels.ToList(),
                count, menuListSearch.page_number, menuListSearch.page_size);
        }



        public async Task<int> InsertMenu(MenuViewModel menuViewModel)
        {
            var menu = _mapper.Map<Menu>(menuViewModel);
            return await _menuRepository.InsertMenu(menu);

        }

        public async Task<bool> UpdateMenu(MenuViewModel menuViewModel)
        {
            if (menuViewModel.id > 0)
            {
                var menu = _mapper.Map<Menu>(menuViewModel);
                return await _menuRepository.UpdateMenu(menu);
            }
            return false;

        }
    }
}
