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
    public class NutrientService : INutrientService
    {
        private readonly INutrientRepository _nutrientRepository;
        private readonly IMapper _mapper;
        public NutrientService(INutrientRepository nutrientRepository, IMapper mapper)
        {
            _nutrientRepository = nutrientRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteNutrient(int id)
        {
            var nutrientCheck = await _nutrientRepository.GetNutrient(id);
            if (nutrientCheck == null)
            {
                return false;
            }
            else
            {
                return await _nutrientRepository.DeleteNutrient(id);
            }
        }

        public async Task<NutrientViewModel> GetNutrient(int id)
        {
            var nutrient = await _nutrientRepository.GetNutrient(id);
            var nutrientViewModel = _mapper.Map<NutrientViewModel>(nutrient);
            return nutrientViewModel;
            
        }

        public async Task<PagedList<NutrientViewModel>> GetNutrients(NutrientListSearch nutrientListSearch)
        {
            var nutrients = await _nutrientRepository.GetNutrients();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = nutrients.Count();

            var dataPage = nutrients
                        .Skip((nutrientListSearch.page_number - 1) * nutrientListSearch.page_size)
              .Take(nutrientListSearch.page_size);

            var nutrientViewModel = _mapper.Map<IEnumerable<NutrientViewModel>>(dataPage);

            return new PagedList<NutrientViewModel>(nutrientViewModel.ToList(),
                count, nutrientListSearch.page_number, nutrientListSearch.page_size);
        }

        public async Task InsertNutrient(NutrientViewModel nutrientViewModel)
        {
            var nutrient = _mapper.Map<Nutrient>(nutrientViewModel);
            await _nutrientRepository.InsertNutrient(nutrient);
        }

        public async Task<bool> UpdateNutrient(NutrientViewModel nutrientViewModel)
        {
            var nutrient = _mapper.Map<Nutrient>(nutrientViewModel);
            return await _nutrientRepository.UpdateNutrient(nutrient);
        }
    }
}
