using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface INutrientDetailService
    {
        Task<NutrientDetailViewModel> GetNutrientDetail(int id);
        Task InsertNutrientDetail(NutrientDetailViewModel nutrientDetailViewModel);
        Task<bool> UpdateNutrientDetail(NutrientDetailViewModel nutrientDetailViewModel);
        Task<bool> DeleteNutrientDetail(int id);
        Task<PagedList<NutrientDetailViewModel>> GetNutrientDetails(NutrientDetailListSearch nutrientDetailListSearch);
    }
}
