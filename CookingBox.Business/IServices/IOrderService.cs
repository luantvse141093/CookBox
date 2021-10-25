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
    public interface IOrderService
    {
        Task<OrderViewModel> GetOrder(int id);
        Task<int> InsertOrder(OrderViewModel OrderViewModel);
        Task<bool> UpdateOrder(int id, string status, string note);
        Task<PagedList<OrderViewModel>> GetOrders(OrderListSearch OrderListSearch);



    }
}
