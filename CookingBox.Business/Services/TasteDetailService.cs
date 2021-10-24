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
    public class TasteDetailService : ITasteDetailService
    {
        private readonly ITasteDetailRepository _tasteDetailRepository;
        private readonly IMapper _mapper;
        public TasteDetailService(ITasteDetailRepository tasteDetailRepository, IMapper mapper)
        {
            _tasteDetailRepository = tasteDetailRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteTasteDetail(int id)
        {
            var tasteDetailCheck = await _tasteDetailRepository.GetTasteDetail(id);
            if (tasteDetailCheck == null)
            {
                return false;
            }
            else
            {
                return await _tasteDetailRepository.DeleteTasteDetail(id);
            }
        }

        public async Task<TasteDetailViewModel> GetTasteDetail(int id)
        {
            var tasteDetail = await _tasteDetailRepository.GetTasteDetail(id);
            var tasteDetailViewModel = _mapper.Map<TasteDetailViewModel>(tasteDetail);
            return tasteDetailViewModel;
            
        }

        public async Task<PagedList<TasteDetailViewModel>> GetTasteDetails(TasteDetailListSearch tasteDetailListSearch)
        {
            var tasteDetails = await _tasteDetailRepository.GetTasteDetails();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = tasteDetails.Count();

            var dataPage = tasteDetails
                        .Skip((tasteDetailListSearch.page_number - 1) * tasteDetailListSearch.page_size)
              .Take(tasteDetailListSearch.page_size);

            var tasteDetailViewModels = _mapper.Map<IEnumerable<TasteDetailViewModel>>(dataPage);

            return new PagedList<TasteDetailViewModel>(tasteDetailViewModels.ToList(),
                count, tasteDetailListSearch.page_number, tasteDetailListSearch.page_size);

        }

        public async Task InsertTasteDetail(TasteDetailViewModel tasteDetailViewModel)
        {
            var tasteDetail = _mapper.Map<TasteDetail>(tasteDetailViewModel);
            await _tasteDetailRepository.InsertTasteDetail(tasteDetail);
        }

        public async Task<bool> UpdateTasteDetail(TasteDetailViewModel tasteDetailViewModel)
        {
            var tasteDetail = _mapper.Map<TasteDetail>(tasteDetailViewModel);
            return await _tasteDetailRepository.UpdateTasteDetail(tasteDetail);
        }
    }
}
