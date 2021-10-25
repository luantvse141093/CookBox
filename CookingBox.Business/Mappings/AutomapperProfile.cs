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
                        opt => opt.MapFrom(source => source.parent_id));

            //dish user
            CreateMap<Dish, DishUserViewModel>()
               .ForMember(dest => dest.category_name,
                       opt => opt.MapFrom(source => source.Category.Name));
            //category
            CreateMap<Category, CategoryViewModel>()
                .ReverseMap();

            //DishIngredientViewModel
            CreateMap<DishIngredient, DishIngredientViewModel>().ReverseMap();

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
            CreateMap<Menu, MenuViewModel>()
                .ForMember(dest => dest.TimeFrom,
                        opt => opt.MapFrom(source => source.Session.TimeFrom))
                .ForMember(dest => dest.TimeTo,
                        opt => opt.MapFrom(source => source.Session.TimeTo))
               ;

            CreateMap<MenuViewModel, Menu>()
                .ForMember(dest => dest.SessionId,
                        opt => opt.MapFrom(source => source.session_id))

                .ForMember(dest => dest.MenuDetails,
                        opt => opt.MapFrom(source => source.menu_details))
                ;

            //MenuDetail
            CreateMap<MenuDetail, MenuDetailViewModel>()
                .ReverseMap();

            //OrderDetail
            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ReverseMap();

            //order
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.order_status,
                        opt => opt.MapFrom(source => source.OrderStatus)).ReverseMap();
            //CreateMap<OrderViewModel, Order>();

            //NutrientDetail
            CreateMap<NutrientDetail, NutrientDetailViewModel>()
                .ReverseMap();

            //TasteDetail
            CreateMap<TasteDetail, TasteDetailViewModel>()
                .ReverseMap();

        }
    }
}
