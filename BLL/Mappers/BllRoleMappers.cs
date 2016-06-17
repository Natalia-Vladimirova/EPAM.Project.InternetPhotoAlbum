using DAL.Interfaces.DataTransferObjects;
using BLL.Interfaces.Entities;

namespace BLL.Mappers
{
    public static class BllRoleMappers
    {
        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            if (roleEntity == null) return null;

            return new DalRole()
            {
                Id = roleEntity.Id,
                RoleName = roleEntity.RoleName
            };
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null) return null;

            return new RoleEntity()
            {
                Id = dalRole.Id,
                RoleName = dalRole.RoleName
            };
        }
    }
}
