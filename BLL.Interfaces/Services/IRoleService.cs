using System.Collections.Generic;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IRoleService : IService<RoleEntity>
    {
        IEnumerable<RoleEntity> GetAllRoleEntities();
        IEnumerable<RoleEntity> GetUserRoleEntities(int userId);
        IEnumerable<UserEntity> GetUserEntitiesInRole(string roleName);
        void AddUserEntityToRole(int userId, string roleName);
    }
}
