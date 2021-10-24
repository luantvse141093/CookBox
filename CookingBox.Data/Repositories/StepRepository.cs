using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Data.Repositories
{
    public class StepRepository : IStepRepository
    {
        private readonly CookBoxContext _context;

        public StepRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteStep(int id)
        {
            var currentStep = await GetStep(id);
            _context.Steps.Remove(currentStep);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Step> GetStep(int id)
        {
            var step = await _context.Steps
                  .FirstOrDefaultAsync(x => x.Id == id);
            return step;
        }

        public async Task<IEnumerable<Step>> GetSteps()
        {
            var steps = await _context.Steps
                  .ToListAsync();
            return steps;
        }

        public async Task InsertStep(Step step)
        {
            await _context.Steps.AddAsync(step);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStep(Step step)
        {
            _context.Steps.Update(step);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
