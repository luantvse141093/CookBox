using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CookingBox.Data.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly CookBoxContext _context;

        public OrderDetailRepository(CookBoxContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteOrderDetail(int id)
        {
            var currentOrderDetail = await GetOrderDetail(id);
            _context.OrderDetails.Remove(currentOrderDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<OrderDetail> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails
                  .FirstOrDefaultAsync(x => x.Id == id);
            return orderDetail;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            var orderDetails = await _context.OrderDetails
                  .Include(x => x.Dish)
                  .ToListAsync();
            return orderDetails;
        }

        public async Task InsertOrderDetail(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
