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
    public class DishIngredientService : IDishIngredientService
    {
        private readonly IDishIngredientRepository _dishIngredientRepository;
        private readonly IMapper _mapper;
        public DishIngredientService(IDishIngredientRepository dishIngredientRepository, IMapper mapper)
        {
            _dishIngredientRepository = dishIngredientRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteDishIngredient(int id)
        {
            var categoryCheck = await _dishIngredientRepository.GetDishIngredient(id);
            if (categoryCheck == null)
            {
                return false;
            }
            else
            {
                return await _dishIngredientRepository.DeleteDishIngredient(id);
            }
        }

        public async Task<DishIngredientViewModel> GetDishIngredient(int id)
        {
            var dishIngredient = await _dishIngredientRepository.GetDishIngredient(id);
            var dishIngredientViewModel = _mapper.Map<DishIngredientViewModel>(dishIngredient);
            return dishIngredientViewModel;
            
        }

        public async Task<PagedList<DishIngredientViewModel>> GetDishIngredients(DishIngredientListSearch dishIngredientListSearch)
        {
            var dishIngredients = await _dishIngredientRepository.GetDishIngredients();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = dishIngredients.Count();

            var dataPage = dishIngredients
                        .Skip((dishIngredientListSearch.page_number - 1) * dishIngredientListSearch.page_size)
              .Take(dishIngredientListSearch.page_size);

            var dishIngredientViewModel = _mapper.Map<IEnumerable<DishIngredientViewModel>>(dataPage);

            return new PagedList<DishIngredientViewModel>(dishIngredientViewModel.ToList(),
                count, dishIngredientListSearch.page_number, dishIngredientListSearch.page_size);

        }

        public async Task InsertDishIngredient(DishIngredientViewModel dishIngredientViewModel)
        {
            var dishIngredient = _mapper.Map<DishIngredient>(dishIngredientViewModel);
            await _dishIngredientRepository.InsertDishIngredient(dishIngredient);
        }

        public async Task<bool> UpdateDishIngredient(DishIngredientViewModel dishIngredientViewModel)
        {
            var dishIngredient = _mapper.Map<DishIngredient>(dishIngredientViewModel);
            return await _dishIngredientRepository.UpdateDishIngredient(dishIngredient);
        }
    }
}
