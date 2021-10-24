using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CookingBox.Business.IServices
{
    public interface IPaymentService
    {
        Task<PaymentViewModel> GetPayment(string id);
        Task InsertPayment(PaymentViewModel paymentViewModel);
        Task<bool> UpdatePayment(PaymentViewModel paymentViewModel);
        Task<bool> DeletePayment(string id);
        Task<PagedList<PaymentViewModel>> GetPayments(PaymentListSearch paymentListSearch);
    }
}
