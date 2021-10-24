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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteCategory(int id)
        {
            var categoryCheck = await _categoryRepository.GetCategory(id);
            if (categoryCheck == null)
            {
                return false;
            }
            else
            {
                return await _categoryRepository.DeleteCategory(id);
            }
        }

        public async Task<PagedList<CategoryViewModel>> GetCategories(CategoryListSearch categoryListSearch)
        {
            var categories = await _categoryRepository.GetCategories();

            if (!string.IsNullOrEmpty(categoryListSearch.name))
            {
                categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            }

            var count = categories.Count();

            var dataPage = categories
                        .Skip((categoryListSearch.page_number - 1) * categoryListSearch.page_size)
              .Take(categoryListSearch.page_size);

            var categoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(dataPage);

            return new PagedList<CategoryViewModel>(categoryViewModels.ToList(),
                count, categoryListSearch.page_number, categoryListSearch.page_size);
        }

        public async Task<CategoryViewModel> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategory(id);
            var categoryViewModel = _mapper.Map<CategoryViewModel>(category);
            return categoryViewModel;
        }

        public async Task InsertCategory(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.InsertCategory(category);
        }

        public async Task<bool> UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            return await _categoryRepository.UpdateCategory(category);
        }
    }
}
