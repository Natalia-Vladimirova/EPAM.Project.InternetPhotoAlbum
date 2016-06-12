using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IRoleService
    {
        RoleEntity GetRoleEntity(int id);
        IEnumerable<RoleEntity> GetAllRoleEntities();
        void CreateRole(RoleEntity user);
        void DeleteRole(RoleEntity user);
        IEnumerable<RoleEntity> GetUserRoleEntities(int userId);
        IEnumerable<UserEntity> GetUserEntitiesInRole(string roleName);
        void AddUserEntityToRole(int userId, string roleName);
    }
}
