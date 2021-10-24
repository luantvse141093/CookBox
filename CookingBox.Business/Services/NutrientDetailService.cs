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
    public class NutrientDetailService : INutrientDetailService
    {
        private readonly INutrientDetailRepository _nutrientDetailRepository;
        private readonly IMapper _mapper;
        public NutrientDetailService(INutrientDetailRepository nutrientDetailRepository, IMapper mapper)
        {
            _nutrientDetailRepository = nutrientDetailRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteNutrientDetail(int id)
        {
            var nutrientDetailCheck = await _nutrientDetailRepository.GetNutrientDetail(id);
            if (nutrientDetailCheck == null)
            {
                return false;
            }
            else
            {
                return await _nutrientDetailRepository.DeleteNutrientDetail(id);
            }
        }

        public async Task<NutrientDetailViewModel> GetNutrientDetail(int id)
        {
            var nutrientDetail = await _nutrientDetailRepository.GetNutrientDetail(id);
            var nutrientDetailViewModel = _mapper.Map<NutrientDetailViewModel>(nutrientDetail);
            return nutrientDetailViewModel;
            
        }

        public async Task<PagedList<NutrientDetailViewModel>> GetNutrientDetails(NutrientDetailListSearch nutrientDetailListSearch)
        {
            var nutrientDetails = await _nutrientDetailRepository.GetNutrientDetails();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = nutrientDetails.Count();

            var dataPage = nutrientDetails
                        .Skip((nutrientDetailListSearch.page_number - 1) * nutrientDetailListSearch.page_size)
              .Take(nutrientDetailListSearch.page_size);

            var categoryViewModels = _mapper.Map<IEnumerable<NutrientDetailViewModel>>(dataPage);

            return new PagedList<NutrientDetailViewModel>(categoryViewModels.ToList(),
                count, nutrientDetailListSearch.page_number, nutrientDetailListSearch.page_size);
        }

        public async Task InsertNutrientDetail(NutrientDetailViewModel nutrientDetailViewModel)
        {
            var nutrientDetail = _mapper.Map<NutrientDetail>(nutrientDetailViewModel);
            await _nutrientDetailRepository.InsertNutrientDetail(nutrientDetail);
        }

        public async Task<bool> UpdateNutrientDetail(NutrientDetailViewModel nutrientDetailViewModel)
        {
            var nutrientDetail = _mapper.Map<NutrientDetail>(nutrientDetailViewModel);
            return await _nutrientDetailRepository.UpdateNutrientDetail(nutrientDetail);
        }
    }
}
