using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface IMetarialService
    {
        Task<MetarialViewModel> GetMetarial(int id);
        Task InsertMetarial(MetarialViewModel metarialViewModel);
        Task<bool> UpdateMetarial(MetarialViewModel metarialViewModel);
        Task<bool> DeleteMetarial(int id);
        Task<PagedList<MetarialViewModel>> GetMetarials(MetarialListSearch metarialListSearch);
    }
}
