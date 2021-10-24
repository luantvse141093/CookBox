using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.IServices
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetCategory(int id);
        Task InsertCategory(CategoryViewModel categoryViewModel);
        Task<bool> UpdateCategory(CategoryViewModel categoryViewModel);
        Task<bool> DeleteCategory(int id);
        Task<PagedList<CategoryViewModel>> GetCategories(CategoryListSearch categoryListSearch);
    }
}
