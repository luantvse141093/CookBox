using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;

namespace CookingBox.Data.IRepositories
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetails();
        Task<OrderDetail> GetOrderDetail(int id);
        Task InsertOrderDetail(OrderDetail orderDetail);
        Task<bool> UpdateOrderDetail(OrderDetail orderDetail);
        Task<bool> DeleteOrderDetail(int id);
    }
}
