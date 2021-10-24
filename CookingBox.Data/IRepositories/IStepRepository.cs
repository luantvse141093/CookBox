using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface IStepRepository
    {
        Task<IEnumerable<Step>> GetSteps();
        Task<Step> GetStep(int id);
        Task InsertStep(Step step);
        Task<bool> UpdateStep(Step step);
        Task<bool> DeleteStep(int id);
    }
}
