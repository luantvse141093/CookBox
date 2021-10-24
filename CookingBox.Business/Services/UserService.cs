using CookingBox.Business.ViewModels;
using CookingBox.Data.Entities;
using CookingBox.Data.IRepositories;
using CookingBox.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingBox.Business.IServices;
using AutoMapper;
using CookingBox.Business.CustomEntities.ModelSearch;
using CookingBox.Business.CustomEntities.SeedWork;
using CookingBox.Business.Helppers;

namespace CookingBox.Business.Services
{

    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UserService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var userCheck = await _usersRepository.GetUser(id);
            if (userCheck == null)
            {
                return false;
            }
            else
            {
                return await _usersRepository.DeleteUser(id);
            }

        }

        public async Task<UserViewModel> GetUser(int id)
        {
            var user = await _usersRepository.GetUser(id);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public async Task<UserViewModel> GetUserByEmail(string email)
        {
            var user = await _usersRepository.GetUserByEmail(email);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public async Task<PagedList<UserViewModel>> GetUsers(UserListSearch userListSearch)
        {
            var users = await _usersRepository.GetUsers();


            if (!string.IsNullOrEmpty(userListSearch.role_id))
            {
                users = users.Where(x => x.RoleId.Equals(userListSearch.role_id));
            }
            if (!string.IsNullOrEmpty(userListSearch.email))
            {
                users = users.Where(x => x.Email.Equals(userListSearch.email));
            }
            if (userListSearch.status.HasValue)
            {
                users = users.Where(x => x.Status == userListSearch.status);
            }

            var count = users.Count();

            var dataPage = users
              .Skip((userListSearch.page_number - 1) * userListSearch.page_size)
              .Take(userListSearch.page_size);

            var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(dataPage);

            PagedList<UserViewModel> pagedList = new PagedList<UserViewModel>(userViewModels.ToList(),
                        count, userListSearch.page_number, userListSearch.page_size);
            return pagedList;
        }


        public async Task<int> InsertUser(UserViewModel userViewModel)
        {
            var userCheck = await _usersRepository.GetUser(userViewModel.id);
            //check roleid co ton tai hay ko
            if (userCheck != null)
            {
                throw new Exception("User exits");
            }
            else
            {
                var user = _mapper.Map<User>(userViewModel);
                int id = await _usersRepository.InsertUser(user);
                return id;
            }
        }

        public async Task<bool> UpdateUser(UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            return await _usersRepository.UpdateUser(user);
        }

        public async Task<UserViewModel> Login(string email, string password)
        {
            var user = await _usersRepository.GetUsers();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword("truong123");
            user = user.Where(x => x.Email.ToLower().Equals(email.ToLower()) && BCrypt.Net.BCrypt.Verify(password, "123"));
            if (user.Count() > 0)
            {
                var userViewModel = _mapper.Map<UserViewModel>(user.FirstOrDefault());
                return userViewModel;
            }
            return null;
        }
    }
}