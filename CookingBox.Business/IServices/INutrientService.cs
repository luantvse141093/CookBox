using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface INutrientService
    {
        Task<NutrientViewModel> GetNutrient(int id);
        Task InsertNutrient(NutrientViewModel nutrientViewModel);
        Task<bool> UpdateNutrient(NutrientViewModel nutrientViewModel);
        Task<bool> DeleteNutrient(int id);
        Task<PagedList<NutrientViewModel>> GetNutrients(NutrientListSearch nutrientListSearch);
    }
}
