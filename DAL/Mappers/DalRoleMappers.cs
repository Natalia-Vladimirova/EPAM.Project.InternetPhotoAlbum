﻿using ORM;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Mappers
{
    public static class DalRoleMappers
    {
        public static Role ToOrmRole(this DalRole dalRole)
        {
            if (dalRole == null) return null;

            return new Role()
            {
                Id = dalRole.Id,
                Name = dalRole.RoleName
            };
        }

        public static DalRole ToDalRole(this Role role)
        {
            if (role == null) return null;

            return new DalRole()
            {
                Id = role.Id,
                RoleName = role.Name
            };
        }
    }
}
