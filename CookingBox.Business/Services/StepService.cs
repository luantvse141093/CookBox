using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;

namespace CookingBox.Business.Services
{
    public class StepService : IStepService
    {
        private readonly IStepRepository _stepRepository;
        private readonly IMapper _mapper;
        public StepService(IStepRepository stepRepository, IMapper mapper)
        {
            _stepRepository = stepRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteStep(int id)
        {
            var stepCheck = await _stepRepository.GetStep(id);
            if (stepCheck == null)
            {
                return false;
            }
            else
            {
                return await _stepRepository.DeleteStep(id);
            }
        }

        public async Task<StepViewModel> GetStep(int id)
        {
            var step = await _stepRepository.GetStep(id);
            var stepViewModel = _mapper.Map<StepViewModel>(step);
            return stepViewModel;
            
        }

        public async Task<PagedList<StepViewModel>> GetSteps(StepListSearch stepListSearch)
        {
            var steps = await _stepRepository.GetSteps();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = steps.Count();

            var dataPage = steps
                        .Skip((stepListSearch.page_number - 1) * stepListSearch.page_size)
              .Take(stepListSearch.page_size);

            var stepViewModels = _mapper.Map<IEnumerable<StepViewModel>>(dataPage);

            return new PagedList<StepViewModel>(stepViewModels.ToList(),
                count, stepListSearch.page_number, stepListSearch.page_size);
        }

        public async Task InsertStep(StepViewModel stepViewModel)
        {
            var step = _mapper.Map<Step>(stepViewModel);
            await _stepRepository.InsertStep(step);
        }

        public async Task<bool> UpdateStep(StepViewModel stepViewModel)
        {
            var step = _mapper.Map<Step>(stepViewModel);
            return await _stepRepository.UpdateStep(step);
        }
    }
}
