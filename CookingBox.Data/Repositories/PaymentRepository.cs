using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CookBoxContext _context;

        public PaymentRepository(CookBoxContext context)
        {
            _context = context;
        }
        public async Task<bool> DeletePayment(string id)
        {
            var currentPost = await GetPayment(id);
            //currentPost.Status = false;
            _context.Payments.Update(currentPost);
            int rows = await _context.SaveChangesAsync();
            //return rows > 0;
            return true;
        }

        public async Task<Payment> GetPayment(string id)
        {
            var post = await _context.Payments
             .FirstOrDefaultAsync(x => x.Id.ToLower().Equals(id));
            return post;
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
            var posts = await _context.Payments.ToListAsync();
            return posts;
        }

        public async Task InsertPayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
