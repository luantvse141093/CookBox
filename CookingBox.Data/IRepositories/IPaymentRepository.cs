using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.IRepositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPayments();
        Task<Payment> GetPayment(string id);
        Task InsertPayment(Payment payment);
        Task<bool> UpdatePayment(Payment payment);
        Task<bool> DeletePayment(string id);
    }
}
