using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface ITasteDetailService
    {
        Task<TasteDetailViewModel> GetTasteDetail(int id);
        Task InsertTasteDetail(TasteDetailViewModel tasteDetailViewModel);
        Task<bool> UpdateTasteDetail(TasteDetailViewModel tasteDetailViewModel);
        Task<bool> DeleteTasteDetail(int id);
        Task<PagedList<TasteDetailViewModel>> GetTasteDetails(TasteDetailListSearch tasteDetailListSearch);
    }
}
