using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using CookingBox.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Business.IServices;
using AutoMapper;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.ModelSearch.User;
using CookingBox.Business.ViewModels.User;

namespace CookingBox.Business.Services
{

    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public DishService(IDishRepository DishsRepository, IMapper mapper, IMenuRepository menuRepository)
        {
            _dishRepository = DishsRepository;
            _mapper = mapper;
            _menuRepository = menuRepository;
        }

        public async Task<bool> DeleteDish(int id)
        {
            var DishCheck = await _dishRepository.GetDish(id);
            if (DishCheck == null)
            {
                return false;
            }
            else
            {
                return await _dishRepository.DeleteDish(id);
            }

        }

        public async Task<DishViewModel> GetDish(int id)
        {
            var dish = await _dishRepository.GetDish(id);
            if (dish == null)
            {
                return null;
            }
            var dishViewModel = _mapper.Map<DishViewModel>(dish);
            dishViewModel.dish_ingredients = _mapper.Map<ICollection<DishIngredientViewModel>>(dish.DishIngredients);
            dishViewModel.nutrient_details = _mapper.Map<ICollection<NutrientDetailViewModel>>(dish.NutrientDetails);
            dishViewModel.taste_details = _mapper.Map<ICollection<TasteDetailViewModel>>(dish.TasteDetails);
            return dishViewModel;
        }



        public async Task InsertDish(DishViewModel dishViewModel)
        {

            var dish = _mapper.Map<Dish>(dishViewModel);
            await _dishRepository.InsertDish(dish);

        }

        public async Task<bool> UpdateDish(DishViewModel dishViewModel)
        {
            var dish = _mapper.Map<Dish>(dishViewModel);
            return await _dishRepository.UpdateDish(dish);
        }

        public async Task<PagedList<DishViewModel>> GetDishes(DishListSearch dishListSearch)
        {
            var dishes = await _dishRepository.GetDishes();

            if (!string.IsNullOrEmpty(dishListSearch.name))
            {
                dishes = dishes.Where(x => x.Name.ToLower().Contains(dishListSearch.name.ToLower()));
            }
            if (dishListSearch.category_id > 0)
            {
                dishes = dishes.Where(x => x.Category.Id == dishListSearch.category_id);
            }
            if (dishListSearch.status.HasValue)
            {
                dishes = dishes.Where(x => x.Status == dishListSearch.status);
            }
            if (dishListSearch.taste_id > 0)
            {
                dishes = dishes.Where(x => x.TasteDetails

                .Any(z => z.TasteId == dishListSearch.taste_id)

                );
            }
            if (dishListSearch.sort_name.HasValue)
            {
                if (dishListSearch.sort_name.Value == Enums.Sort.asc)
                {
                    dishes = dishes.OrderBy(x => x.Name);
                }
                if (dishListSearch.sort_name.Value == Enums.Sort.desc)
                {
                    dishes = dishes.OrderByDescending(x => x.Name);
                }
            }
            var count = dishes.Count();
            var dataPage = dishes
                        .Skip((dishListSearch.page_number - 1) * dishListSearch.page_size)
              .Take(dishListSearch.page_size);

            var dishViewModels = _mapper.Map<IEnumerable<DishViewModel>>(dataPage);

            return new PagedList<DishViewModel>(dishViewModels.ToList(),
                count, dishListSearch.page_number, dishListSearch.page_size);
        }


        //User---------------------------------------

        public async Task<DishUserViewModel> GetDishUser(UserMenuListSearch userMenuListSearch)
        {
            var menus = await _menuRepository.GetMenus();
            menus = menus.Where(x => x.MenuStores.Any(y => y.StoreId == userMenuListSearch.store_id));

            if (userMenuListSearch.store_id != null && userMenuListSearch.store_id > 0)
            {
                var timeNow = DateTime.Now.Hour + DateTime.Now.Minute * 1.0 / 60;
                var menu = menus
                .FirstOrDefault(x => x.Session.TimeFrom <= timeNow && x.Session.TimeTo >= timeNow);
                if (menu != null)
                {
                    var menudetail = menu.MenuDetails.FirstOrDefault(x => x.DishId == userMenuListSearch.dish_id);
                    if (menudetail != null)
                    {
                        var price = menudetail.Price;
                        var dish = await _dishRepository.GetDish(userMenuListSearch.dish_id);

                        if (dish == null || dish.Status == false || price == null)
                        {
                            return null;
                        }
                        var dishViewModel = _mapper.Map<DishUserViewModel>(dish);
                        dishViewModel.dish_ingredients = _mapper.Map<ICollection<DishIngredientViewModel>>(dish.DishIngredients);
                        dishViewModel.nutrient_details = _mapper.Map<ICollection<NutrientDetailViewModel>>(dish.NutrientDetails);
                        dishViewModel.taste_details = _mapper.Map<ICollection<TasteDetailViewModel>>(dish.TasteDetails);
                        dishViewModel.price = price;
                        return dishViewModel;
                    }

                }

            }
            return null;
        }


        public async Task<PagedList<MenuDetail>> GetDishesUser(UserMenuListSearch userMenuListSearch)
        {
            var menus = await _menuRepository.GetMenus();
            menus = menus.Where(x => x.MenuStores.Any(y => y.StoreId == userMenuListSearch.store_id));

            if (userMenuListSearch.store_id != null && userMenuListSearch.store_id > 0)
            {
                var timeNow = DateTime.Now.Hour + DateTime.Now.Minute * 1.0 / 60;
                var menu = menus
                .FirstOrDefault(x => x.Session.TimeFrom <= timeNow && x.Session.TimeTo >= timeNow);

                if (menu != null)
                {
                    var dishes = menu.MenuDetails
                  .Where(x => x.Dish.Status == true && x.Dish.ParentId == 0
                  && x.Dish.CategoryId == (userMenuListSearch.category_id > 0 ? userMenuListSearch.category_id : x.Dish.CategoryId)
                  && x.Dish.Name.ToLower()
                  .Contains(!string.IsNullOrEmpty(userMenuListSearch.name) ? userMenuListSearch.name.ToLower() : x.Dish.Name.ToLower()));

                    var count = dishes.Count();
                    var dataPage = dishes
                                .Skip((userMenuListSearch.page_number - 1) * userMenuListSearch.page_size)
                      .Take(userMenuListSearch.page_size);

                    return new PagedList<MenuDetail>(dataPage.ToList(),
                        count, userMenuListSearch.page_number, userMenuListSearch.page_size);
                }
            }

            return null;

        }

        //test1 taste

        public async Task<DishUserViewModel> GetDishByTaste(UserMenuListSearch userMenuListSearch)
        {
            var menus = await _menuRepository.GetMenus();
            menus = menus.Where(x => x.MenuStores.Any(y => y.StoreId == userMenuListSearch.store_id));

            if (userMenuListSearch.store_id != null && userMenuListSearch.store_id > 0)
            {
                var timeNow = DateTime.Now.Hour + DateTime.Now.Minute * 1.0 / 60;
                var menu = menus
                .FirstOrDefault(x => x.Session.TimeFrom <= timeNow && x.Session.TimeTo >= timeNow);

                if (menu != null)
                {

                    var listDish = menu.MenuDetails.Where(x =>
                    x.Dish.ParentId == userMenuListSearch.dish_id || x.DishId == userMenuListSearch.dish_id)
                        .ToList();


                    for (int i = 0; i < listDish.Count(); i++)
                    {
                        var dish_idCheck = listDish[i].DishId.GetValueOrDefault();

                        var dish1 = await _dishRepository.GetDish(dish_idCheck);
                        var checkTasteCount = 0;
                        //var test = null;
                        for (int j = 0; j < userMenuListSearch.list_taste.Count(); j++)
                        {
                            var test = dish1.TasteDetails.Where(x => x.TasteId == userMenuListSearch.list_taste[j].TasteId
                        && x.TasteLevel == userMenuListSearch.list_taste[j].TasteLevel);
                            if (test != null && test.Count() != 0)
                            {
                                checkTasteCount++;
                            }
                            else
                            {
                                checkTasteCount = 0;
                                //  test = null;
                            }

                        }


                        if (checkTasteCount == userMenuListSearch.list_taste.Count())
                        {
                            var menudetail = menu.MenuDetails.FirstOrDefault(x => x.DishId == dish_idCheck);
                            if (menudetail != null)
                            {
                                var price = menudetail.Price;
                                var dish = await _dishRepository.GetDish(dish_idCheck);

                                if (dish == null || dish.Status == false || price == null)
                                {
                                    return null;
                                }
                                var dishViewModel = _mapper.Map<DishUserViewModel>(dish);
                                dishViewModel.dish_ingredients = _mapper.Map<ICollection<DishIngredientViewModel>>(dish.DishIngredients);
                                dishViewModel.nutrient_details = _mapper.Map<ICollection<NutrientDetailViewModel>>(dish.NutrientDetails);
                                dishViewModel.taste_details = _mapper.Map<ICollection<TasteDetailViewModel>>(dish.TasteDetails);
                                dishViewModel.price = price;
                                return dishViewModel;

                            }
                        }

                    }

                }

            }


            return null;
        }


    }
}