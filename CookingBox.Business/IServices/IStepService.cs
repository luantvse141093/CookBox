using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface IStepService
    {
        Task<StepViewModel> GetStep(int id);
        Task InsertStep(StepViewModel stepViewModel);
        Task<bool> UpdateStep(StepViewModel stepViewModel);
        Task<bool> DeleteStep(int id);
        Task<PagedList<StepViewModel>> GetSteps(StepListSearch stepListSearch);
    }
}
