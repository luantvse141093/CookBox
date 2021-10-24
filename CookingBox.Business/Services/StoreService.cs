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
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        public StoreService(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteStore(int id)
        {
            var storeCheck = await _storeRepository.GetStore(id);
            if (storeCheck == null)
            {
                return false;
            }
            else
            {
                return await _storeRepository.DeleteStore(id);
            }
        }

        public async Task<StoreViewModel> GetStore(int id)
        {
            var store = await _storeRepository.GetStore(id);
            var storeViewModel = _mapper.Map<StoreViewModel>(store);
            return storeViewModel;
        }

        public async Task<PagedList<StoreViewModel>> GetStores(StoreListSearch storeListSearch)
        {
            var stores = await _storeRepository.GetStores();


            // stores = stores.Where(x => x.Status == true);

            if (!string.IsNullOrEmpty(storeListSearch.name))
            {
                stores = stores.Where(x => x.Name.ToLower().Contains(storeListSearch.name.ToLower()));
            }

            var count = stores.Count();

            var dataPage = stores
                        .Skip((storeListSearch.page_number - 1) * storeListSearch.page_size)
              .Take(storeListSearch.page_size);

            var storeViewModels = _mapper.Map<IEnumerable<StoreViewModel>>(dataPage);

            return new PagedList<StoreViewModel>(storeViewModels.ToList(),
                count, storeListSearch.page_number, storeListSearch.page_size);
        }

        public async Task<int> InsertStore(StoreViewModel storeViewModel)
        {
            var store = _mapper.Map<Store>(storeViewModel);
            var id = await _storeRepository.InsertStore(store);
            return id;
        }

        public async Task<bool> UpdateStore(StoreViewModel storeViewModel)
        {
            var user = _mapper.Map<Store>(storeViewModel);
            return await _storeRepository.UpdateStore(user);
        }
    }
}
