using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CookingBox.Business.IServices
{
    public interface IUserService
    {
        Task<PagedList<UserViewModel>> GetUsers(UserListSearch userListSearch);
        Task<UserViewModel> GetUser(int id);
        Task<UserViewModel> GetUserByEmail(string email);
        Task<int> InsertUser(UserViewModel UserViewModel);
        Task<bool> UpdateUser(UserViewModel UserViewModel);
        Task<bool> DeleteUser(int id);
        Task<UserViewModel> Login(string email, string password);




    }
}
