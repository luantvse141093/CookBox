using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface INutrientRepository
    {
        Task<IEnumerable<Nutrient>> GetNutrients();
        Task<Nutrient> GetNutrient(int id);
        Task InsertNutrient(Nutrient nutrient);
        Task<bool> UpdateNutrient(Nutrient nutrient);
        Task<bool> DeleteNutrient(int id);
    }
}
