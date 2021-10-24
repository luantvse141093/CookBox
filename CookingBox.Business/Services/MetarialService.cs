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
    public class MetarialService : IMetarialService
    {
        private readonly IMetarialRepository _metarialRepository;
        private readonly IMapper _mapper;
        public MetarialService(IMetarialRepository metarialRepository, IMapper mapper)
        {
            _metarialRepository = metarialRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteMetarial(int id)
        {
            var metarialCheck = await _metarialRepository.GetMetarial(id);
            if (metarialCheck == null)
            {
                return false;
            }
            else
            {
                return await _metarialRepository.DeleteMetarial(id);
            }
        }

        public async Task<MetarialViewModel> GetMetarial(int id)
        {
            var metarial = await _metarialRepository.GetMetarial(id);
            var metarialViewModel = _mapper.Map<MetarialViewModel>(metarial);
            return metarialViewModel;
            
        }

        public async Task<PagedList<MetarialViewModel>> GetMetarials(MetarialListSearch metarialListSearch)
        {
            var metarials = await _metarialRepository.GetMetarials();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = metarials.Count();

            var dataPage = metarials
                        .Skip((metarialListSearch.page_number - 1) * metarialListSearch.page_size)
              .Take(metarialListSearch.page_size);

            var metarialViewModels = _mapper.Map<IEnumerable<MetarialViewModel>>(dataPage);

            return new PagedList<MetarialViewModel>(metarialViewModels.ToList(),
                count, metarialListSearch.page_number, metarialListSearch.page_size);

        }

        public async Task InsertMetarial(MetarialViewModel metarialViewModel)
        {
            var metarial = _mapper.Map<Metarial>(metarialViewModel);
            await _metarialRepository.InsertMetarial(metarial);
        }

        public async Task<bool> UpdateMetarial(MetarialViewModel metarialViewModel)
        {
            var metarial = _mapper.Map<Metarial>(metarialViewModel);
            return await _metarialRepository.UpdateMetarial(metarial);
        }
    }
}
