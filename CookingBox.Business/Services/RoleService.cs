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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteRole(string id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return false;
            }
            else
            {
                return await _roleRepository.DeleteRole(id);
            }
        }

        public async Task<RoleViewModel> GetRole(string id)
        {
            var role = await _roleRepository.GetRole(id);
            var roleViewModel = _mapper.Map<RoleViewModel>(role);
            return roleViewModel;
        }

        public async Task<PagedList<RoleViewModel>> GetRoles(RoleListSearch roleListSearch)
        {
            var roles = await _roleRepository.GetRoles();

            if (!string.IsNullOrEmpty(roleListSearch.name))
            {
                roles = roles.Where(x => x.RoleName.ToLower().Contains(roleListSearch.name.ToLower()));
            }
            if (roleListSearch.status.HasValue)
            {
                roles = roles.Where(x => x.Status == roleListSearch.status.Value);
            }

            var count = roles.Count();

            var dataPage = roles
                        .Skip((roleListSearch.page_number - 1) * roleListSearch.page_size)
              .Take(roleListSearch.page_size);

            var roleViewModels = _mapper.Map<IEnumerable<RoleViewModel>>(dataPage);

            return new PagedList<RoleViewModel>(roleViewModels.ToList(),
                count, roleListSearch.page_number, roleListSearch.page_size);
        }

        public async Task InsertRole(RoleViewModel roleViewModel)
        {
            var roleCheck = await _roleRepository.GetRole(roleViewModel.id);
            //check roleid co ton tai hay ko
            if (roleCheck != null)
            {
                throw new Exception("Role exits");
            }
            else
            {
                var role = _mapper.Map<Role>(roleViewModel);
                await _roleRepository.InsertRole(role);
            }
        }

        public async Task<bool> UpdateRole(RoleViewModel roleViewModel)
        {
            var role = _mapper.Map<Role>(roleViewModel);
            return await _roleRepository.UpdateRole(role);
        }
    }
}
