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
    public class RepiceService : IRepiceService
    {
        private readonly IRepiceRepository _repiceRepository;
        private readonly IMapper _mapper;
        public RepiceService(IRepiceRepository repiceRepository, IMapper mapper)
        {
            _repiceRepository = repiceRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteRepice(int id)
        {
            var repiceCheck = await _repiceRepository.GetRepice(id);
            if (repiceCheck == null)
            {
                return false;
            }
            else
            {
                return await _repiceRepository.DeleteRepice(id);
            }
        }

        public async Task<RepiceViewModel> GetRepice(int id)
        {
            var repice = await _repiceRepository.GetRepice(id);
            var repiceViewModel = _mapper.Map<RepiceViewModel>(repice);
            return repiceViewModel;
            
        }

        public async Task<PagedList<RepiceViewModel>> GetRepices(RepiceListSearch repiceListSearch)
        {
            var repices = await _repiceRepository.GetRepices();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = repices.Count();

            var dataPage = repices
                        .Skip((repiceListSearch.page_number - 1) * repiceListSearch.page_size)
              .Take(repiceListSearch.page_size);

            var repiceViewModels = _mapper.Map<IEnumerable<RepiceViewModel>>(dataPage);

            return new PagedList<RepiceViewModel>(repiceViewModels.ToList(),
                count, repiceListSearch.page_number, repiceListSearch.page_size);

        }

        public async Task InsertRepice(RepiceViewModel repiceViewModel)
        {
            var repice = _mapper.Map<Repice>(repiceViewModel);
            await _repiceRepository.InsertRepice(repice);
        }

        public async Task<bool> UpdateRepice(RepiceViewModel repiceViewModel)
        {
            var repice = _mapper.Map<Repice>(repiceViewModel);
            return await _repiceRepository.UpdateRepice(repice);
        }
    }
}
