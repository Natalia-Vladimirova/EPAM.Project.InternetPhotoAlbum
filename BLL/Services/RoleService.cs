using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository repository;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public void CreateEntity(RoleEntity user)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(RoleEntity user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleEntity> GetAllRoleEntities()
        {
            throw new NotImplementedException();
        }

        public RoleEntity GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(RoleEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoleEntity> GetUserRoleEntities(int userId)
        {
            return repository.GetUserRoles(userId)?.Select(role => role.ToBllRole());
        }

        public IEnumerable<UserEntity> GetUserEntitiesInRole(string roleName)
        {
            return repository.GetUsersInRole(roleName)?.Select(user => user.ToBllUser());
        }

        public void AddUserEntityToRole(int userId, string roleName)
        {
            repository.AddUserToRole(userId, roleName);
            uow.Commit();
        }
    }
}
