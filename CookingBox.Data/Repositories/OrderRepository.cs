using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CookingBox.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CookBoxContext _context;

        public OrderRepository(CookBoxContext context)
        {
            _context = context;
        }

        public Task<bool> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> GetOrder(int id)
        {
            var Order = await _context.Orders
                   .Include(x => x.Payment)
                   .Include(x => x.User)
                   .Include(x => x.Store)
                   .FirstOrDefaultAsync(x => x.Id == id);
            return Order;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var Orders = await _context.Orders
                .Include(x => x.Payment)
                .Include(x => x.User)
                .Include(x => x.Store)
                  .ToListAsync();
            return Orders;
        }

        public async Task<int> InsertOrder(Order Order)
        {

            await _context.Orders.AddAsync(Order);
            _context.Entry(Order.Payment).State = EntityState.Unchanged;
            _context.Entry(Order.Store).State = EntityState.Unchanged;
            _context.Entry(Order.User).State = EntityState.Unchanged;
            foreach (var item in Order.OrderDetails)
            {
                _context.Entry(item.Dish).State = EntityState.Unchanged;
            }
            await _context.SaveChangesAsync();
            return Order.Id;
        }

        public async Task<bool> UpdateOrder(Order Order)
        {
            _context.Orders.Update(Order);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
