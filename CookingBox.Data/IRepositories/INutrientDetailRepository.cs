using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface INutrientDetailRepository
    {
        Task<IEnumerable<NutrientDetail>> GetNutrientDetails();
        Task<NutrientDetail> GetNutrientDetail(int id);
        Task InsertNutrientDetail(NutrientDetail nutrientDetail);
        Task<bool> UpdateNutrientDetail(NutrientDetail nutrientDetail);
        Task<bool> DeleteNutrientDetail(int id);
    }
}
