using AutoMapper;
using CookingBox.Business.ViewModels;
using CookingBox.Business.ViewModels.User;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBox.Business.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //user
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.role_name,
                        opt => opt.MapFrom(source => source.Role.RoleName));
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.RoleId,
                        opt => opt.MapFrom(source => source.role_id));

            //dish
            CreateMap<Dish, DishViewModel>()
                .ForMember(dest => dest.category_name,
                        opt => opt.MapFrom(source => source.Category.Name));
            CreateMap<DishViewModel, Dish>()
                .ForMember(dest => dest.CategoryId,
                        opt => opt.MapFrom(source => source.category_id))
                .ForMember(dest => dest.ParentId,
                        opt => opt.MapFrom(source => source.parent_id))
                .ForMember(dest => dest.TasteDetails,
                        opt => opt.MapFrom(source => source.taste_details))
                .ForMember(dest => dest.NutrientDetails,
                        opt => opt.MapFrom(source => source.nutrient_details))
                .ForMember(dest => dest.DishIngredients,
                        opt => opt.MapFrom(source => source.dish_ingredients))
                .ForMember(dest => dest.Repices,
                        opt => opt.MapFrom(source => source.repices));

            //dish user
            CreateMap<Dish, DishUserViewModel>()
               .ForMember(dest => dest.category_name,
                       opt => opt.MapFrom(source => source.Category.Name));

            //DishUserViewModel
            CreateMap<DishViewModel, DishUserViewModel>();


            //category
            CreateMap<Category, CategoryViewModel>()
                .ReverseMap();


            //role
            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>()
                 .ForMember(dest => dest.RoleName,
                        opt => opt.MapFrom(source => source.role_name));


            //store
            CreateMap<Store, StoreViewModel>()
                .ReverseMap();





            //payment
            CreateMap<Payment, PaymentViewModel>()
                .ReverseMap();

            //session
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.id,
                        opt => opt.MapFrom(source => source.Id))
                .ForMember(dest => dest.name,
                        opt => opt.MapFrom(source => source.Name))
                .ForMember(dest => dest.time_from,
                        opt => opt.MapFrom(source => source.TimeFrom))
                .ForMember(dest => dest.time_to,
                        opt => opt.MapFrom(source => source.TimeTo))
                ;

            //Menu
            CreateMap<Menu, MenuViewModel>();

            CreateMap<MenuViewModel, Menu>()

                .ForMember(dest => dest.MenuDetails,
                        opt => opt.MapFrom(source => source.menu_details))
                .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(source => source.name))
                .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(source => source.status));

            //MenuDetail
            CreateMap<MenuDetail, MenuDetailViewModel>().ReverseMap();


            CreateMap<MenuStore, MenuStoreViewModel>()
                 .ForMember(dest => dest.time_from,
                        opt => opt.MapFrom(source => source.Session.TimeFrom))
                 .ForMember(dest => dest.time_to,
                        opt => opt.MapFrom(source => source.Session.TimeTo))
                .ReverseMap();

            //OrderDetail
            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ReverseMap();

            //order
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.order_status,
                        opt => opt.MapFrom(source => source.OrderStatus)).ReverseMap();
            //CreateMap<OrderViewModel, Order>();

            //DishIngredient
            CreateMap<DishIngredient, DishIngredientViewModel>();

            CreateMap<DishIngredientViewModel, DishIngredient>()
               .ForMember(dest => dest.MetarialId,
                        opt => opt.MapFrom(source => source.metarial_id))
               .ForMember(dest => dest.DishId,
                        opt => opt.MapFrom(source => source.dish_id));


            //NutrientDetail
            CreateMap<NutrientDetail, NutrientDetailViewModel>();

            CreateMap<NutrientDetailViewModel, NutrientDetail>()
                 .ForMember(dest => dest.NutrientId,
                        opt => opt.MapFrom(source => source.nutrient_id))
               .ForMember(dest => dest.DishId,
                        opt => opt.MapFrom(source => source.dish_id));

            //TasteDetail
            CreateMap<TasteDetail, TasteDetailViewModel>();
            CreateMap<TasteDetailViewModel, TasteDetail>()
                 .ForMember(dest => dest.TasteId,
                        opt => opt.MapFrom(source => source.taste_id))
                 .ForMember(dest => dest.TasteLevel,
                        opt => opt.MapFrom(source => source.taste_level));

            //Metarial
            CreateMap<Metarial, MetarialViewModel>()
                .ReverseMap();

            //Taste
            CreateMap<Taste, TasteViewModel>()
                .ReverseMap();

            //Nutrient
            CreateMap<Nutrient, NutrientViewModel>()
                .ReverseMap();

        }
    }
}
