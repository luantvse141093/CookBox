using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface IRepiceService
    {
        Task<RepiceViewModel> GetRepice(int id);
        Task InsertRepice(RepiceViewModel repiceViewModel);
        Task<bool> UpdateRepice(RepiceViewModel repiceViewModel);
        Task<bool> DeleteRepice(int id);
        Task<PagedList<RepiceViewModel>> GetRepices(RepiceListSearch repiceListSearch);
    }
}
