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
using Microsoft.EntityFrameworkCore.Storage;

namespace CookingBox.Business.Services
{

    public class OrderService : IOrderService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository ordersRepository, IMapper mapper, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = ordersRepository;
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }



        public async Task<OrderViewModel> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrder(id);
            var orderViewModel = _mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }



        public async Task<int> InsertOrder(OrderViewModel orderViewModel)
        {

            {
                var idOrder = new Random().Next(1, 10000);

                var orderCheck = await _orderRepository.GetOrder(idOrder);
                while (orderCheck != null)
                {
                    idOrder = new Random().Next(1, 10000);
                    orderCheck = await _orderRepository.GetOrder(idOrder);
                }

                orderViewModel.id = idOrder;

                var order = _mapper.Map<Order>(orderViewModel);
                await _orderRepository.InsertOrder(order);
                return order.Id;

            }
        }

        public async Task<bool> UpdateOrder(int id, string newStatus, string note)
        {
            var order = _orderRepository.GetOrder(id);
            if (order.Result != null && "new".Equals(order.Result.OrderStatus.ToLower().ToString()))
            {
                order.Result.OrderStatus = newStatus;
                order.Result.Note = note;
                return await _orderRepository.UpdateOrder(order.Result);
            }
            return false;


        }

        public async Task<PagedList<OrderViewModel>> GetOrders(OrderListSearch orderListSearch)
        {
            var orders = await _orderRepository.GetOrders();


            if (orderListSearch.date.HasValue)
            {
                orders = orders.Where(x => DateTime.Equals(orderListSearch.date, x.Date));

            }
            else if (!orderListSearch.date.HasValue && orderListSearch.sort_date.HasValue)
            {
                if (orderListSearch.sort_date.Value == Enums.Sort.asc)
                {
                    orders = orders.OrderBy(x => x.Date);
                }
                if (orderListSearch.sort_date.Value == Enums.Sort.desc)
                {
                    orders = orders.OrderByDescending(x => x.Date);
                }
            }

            if (orderListSearch.order_status.HasValue)
            {

                orders = orders.Where(x => x.OrderStatus.Equals(orderListSearch.order_status.Value.ToString()));
            }

            if (orderListSearch.user_id > 0)
            {
                orders = orders.Where(x => x.UserId == orderListSearch.user_id);
            }
            if (orderListSearch.store_id > 0)
            {
                orders = orders.Where(x => x.StoreId == orderListSearch.store_id);
            }
            var count = orders.Count();

            var dataPage = orders
                        .Skip((orderListSearch.page_number - 1) * orderListSearch.page_size)
              .Take(orderListSearch.page_size);

            var orderViewModels = _mapper.Map<IEnumerable<OrderViewModel>>(dataPage);

            return new PagedList<OrderViewModel>(orderViewModels.ToList(),
                count, orderListSearch.page_number, orderListSearch.page_size);
        }

    }
}