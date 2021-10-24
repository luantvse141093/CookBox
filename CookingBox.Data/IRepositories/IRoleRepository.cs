using CookingBox.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBox.Data.IRepositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(string id);
        Task InsertRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(string id);
    }
}
