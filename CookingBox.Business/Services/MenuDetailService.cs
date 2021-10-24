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
    public class MenuDetailService : IMenuDetailService
    {
        private readonly IMenuDetailRepository _menuDetailRepository;
        private readonly IMapper _mapper;
        public MenuDetailService(IMenuDetailRepository menuDetailRepository, IMapper mapper)
        {
            _menuDetailRepository = menuDetailRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteMenuDetail(int id)
        {
            var menuDetailCheck = await _menuDetailRepository.GetMenuDetail(id);
            if (menuDetailCheck == null)
            {
                return false;
            }
            else
            {
                return await _menuDetailRepository.DeleteMenuDetail(id);
            }
        }

        public async Task<MenuDetailViewModel> GetMenuDetail(int id)
        {
            var menuDetail = await _menuDetailRepository.GetMenuDetail(id);
            var menuDetailViewModel = _mapper.Map<MenuDetailViewModel>(menuDetail);
            return menuDetailViewModel;

        }

        public async Task<PagedList<MenuDetailViewModel>> GetMenuDetails(MenuDetailListSearch menuDetailListSearch)
        {
            var menuDetails = await _menuDetailRepository.GetMenuDetails();

            var count = menuDetails.Count();

            var dataPage = menuDetails
                        .Skip((menuDetailListSearch.page_number - 1) * menuDetailListSearch.page_size)
              .Take(menuDetailListSearch.page_size);

            var menuDetailViewModels = _mapper.Map<IEnumerable<MenuDetailViewModel>>(dataPage);

            return new PagedList<MenuDetailViewModel>(menuDetailViewModels.ToList(),
                count, menuDetailListSearch.page_number, menuDetailListSearch.page_size);

        }

        public async Task InsertMenuDetail(MenuDetailViewModel menuDetailViewModel)
        {
            var menuDetail = _mapper.Map<MenuDetail>(menuDetailViewModel);
            await _menuDetailRepository.InsertMenuDetail(menuDetail);
        }

        public async Task<bool> UpdateMenuDetail(MenuDetailViewModel menuDetailViewModel)
        {
            var menuDetail = _mapper.Map<MenuDetail>(menuDetailViewModel);
            return await _menuDetailRepository.UpdateMenuDetail(menuDetail);
        }
    }
}
