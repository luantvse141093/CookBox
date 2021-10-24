using System;
using System.Threading.Tasks;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;

namespace CookingBox.Business.IServices
{
    public interface IOrderDetailService
    {
        Task<OrderDetailViewModel> GetOrderDetail(int id);
        Task InsertOrderDetail(OrderDetailViewModel orderDetailViewModel);
        Task<bool> UpdateOrderDetail(OrderDetailViewModel orderDetailViewModel);
        Task<bool> DeleteOrderDetail(int id);
        Task<PagedList<OrderDetailViewModel>> GetOrderDetails(OrderDetailListSearch orderDetailListSearch);
    }
}
