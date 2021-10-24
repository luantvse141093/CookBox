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
    public class TasteService : ITasteService
    {
        private readonly ITasteRepository _tasteRepository;
        private readonly IMapper _mapper;
        public TasteService(ITasteRepository tasteRepository, IMapper mapper)
        {
            _tasteRepository = tasteRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteTaste(int id)
        {
            var tasteCheck = await _tasteRepository.GetTaste(id);
            if (tasteCheck == null)
            {
                return false;
            }
            else
            {
                return await _tasteRepository.DeleteTaste(id);
            }
        }

        public async Task<TasteViewModel> GetTaste(int id)
        {
            var taste = await _tasteRepository.GetTaste(id);
            var tasteViewModel = _mapper.Map<TasteViewModel>(taste);
            return tasteViewModel;
            
        }

        public async Task<PagedList<TasteViewModel>> GetTastes(TasteListSearch tasteListSearch)
        {
            var tastes = await _tasteRepository.GetTastes();

            // if (!string.IsNullOrEmpty(categoryListSearch.name))
            //{
            //   categories = categories.Where(x => x.Name.ToLower().Contains(categoryListSearch.name.ToLower()));
            //}

            var count = tastes.Count();

            var dataPage = tastes
                        .Skip((tasteListSearch.page_number - 1) * tasteListSearch.page_size)
              .Take(tasteListSearch.page_size);

            var tasteViewModels = _mapper.Map<IEnumerable<TasteViewModel>>(dataPage);

            return new PagedList<TasteViewModel>(tasteViewModels.ToList(),
                count, tasteListSearch.page_number, tasteListSearch.page_size);

        }

        public async Task InsertTaste(TasteViewModel tasteViewModel)
        {
            var taste = _mapper.Map<Taste>(tasteViewModel);
            await _tasteRepository.InsertTaste(taste);
        }

        public async Task<bool> UpdateTaste(TasteViewModel tasteViewModel)
        {
            var taste = _mapper.Map<Taste>(tasteViewModel);
            return await _tasteRepository.UpdateTaste(taste);
        }
    }
}
