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
    public interface IRoleService
    {
        Task<RoleViewModel> GetRole(string id);
        Task InsertRole(RoleViewModel roleViewModel);
        Task<bool> UpdateRole(RoleViewModel roleViewModel);
        Task<bool> DeleteRole(string id);
        Task<PagedList<RoleViewModel>> GetRoles(RoleListSearch roleListSearch);
    }
}
