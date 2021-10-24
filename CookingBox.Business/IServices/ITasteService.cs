using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface ITasteService
    {
        Task<TasteViewModel> GetTaste(int id);
        Task InsertTaste(TasteViewModel tasteViewModel);
        Task<bool> UpdateTaste(TasteViewModel tasteViewModel);
        Task<bool> DeleteTaste(int id);
        Task<PagedList<TasteViewModel>> GetTastes(TasteListSearch tasteListSearch);
    }
}
