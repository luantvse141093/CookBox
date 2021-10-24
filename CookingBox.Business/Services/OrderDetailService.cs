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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteOrderDetail(int id)
        {
            var orderDetailCheck = await _orderDetailRepository.GetOrderDetail(id);
            if (orderDetailCheck == null)
            {
                return false;
            }
            else
            {
                return await _orderDetailRepository.DeleteOrderDetail(id);
            }
        }

        public async Task<OrderDetailViewModel> GetOrderDetail(int id)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetail(id);
            var orderDetailViewModel = _mapper.Map<OrderDetailViewModel>(orderDetail);
            return orderDetailViewModel;

        }

        public async Task<PagedList<OrderDetailViewModel>> GetOrderDetails(OrderDetailListSearch orderDetailListSearch)
        {
            var orderDetails = await _orderDetailRepository.GetOrderDetails();

            if (orderDetailListSearch.order_id > 0)
            {
                orderDetails = orderDetails.Where(x => x.OrderId == orderDetailListSearch.order_id);
            }

            var count = orderDetails.Count();

            var dataPage = orderDetails
                        .Skip((orderDetailListSearch.page_number - 1) * orderDetailListSearch.page_size)
              .Take(orderDetailListSearch.page_size);

            var orderDetailViewModels = _mapper.Map<IEnumerable<OrderDetailViewModel>>(dataPage);

            return new PagedList<OrderDetailViewModel>(orderDetailViewModels.ToList(),
                count, orderDetailListSearch.page_number, orderDetailListSearch.page_size);

        }

        public async Task InsertOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailViewModel);
            await _orderDetailRepository.InsertOrderDetail(orderDetail);
        }

        public async Task<bool> UpdateOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailViewModel);
            return await _orderDetailRepository.UpdateOrderDetail(orderDetail);
        }
    }
}
