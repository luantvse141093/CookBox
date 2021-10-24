using AutoMapper;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.IServices;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeletePayment(string id)
        {
            var paymentCheck = await _paymentRepository.GetPayment(id);
            if (paymentCheck == null)
            {
                return false;
            }
            else
            {
                return await _paymentRepository.DeletePayment(id);
            }
        }

        public async Task<PaymentViewModel> GetPayment(string id)
        {
            var payment = await _paymentRepository.GetPayment(id);
            var paymentViewModel = _mapper.Map<PaymentViewModel>(payment);
            return paymentViewModel;
        }

        public async Task<PagedList<PaymentViewModel>> GetPayments(PaymentListSearch paymentListSearch)
        {
            var payments = await _paymentRepository.GetPayments();

            if (!string.IsNullOrEmpty(paymentListSearch.name))
            {
                payments = payments.Where(x => x.Name.ToLower().Contains(paymentListSearch.name.ToLower()));
            }

            var count = payments.Count();

            var dataPage = payments
                        .Skip((paymentListSearch.page_number - 1) * paymentListSearch.page_size)
              .Take(paymentListSearch.page_size);

            var paymetViewModels = _mapper.Map<IEnumerable<PaymentViewModel>>(dataPage);

            return new PagedList<PaymentViewModel>(paymetViewModels.ToList(),
                count, paymentListSearch.page_number, paymentListSearch.page_size);
        }

        public async Task InsertPayment(PaymentViewModel paymentViewModel)
        {
            var payment = _mapper.Map<Payment>(paymentViewModel);
            await _paymentRepository.InsertPayment(payment);
        }

        public async Task<bool> UpdatePayment(PaymentViewModel paymentViewModel)
        {
            var payment = _mapper.Map<Payment>(paymentViewModel);
            return await _paymentRepository.UpdatePayment(payment);
        }
    }
}
