using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces.DataTransferObjects;
using DAL.Interfaces.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalRole GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalRole> GetUserRoles(int userId)
        {
            return context.Set<User>().FirstOrDefault(user => user.Id == userId)?.Roles.Select(role => role.ToDalRole());
        }

        public IEnumerable<DalUser> GetUsersInRole(string roleName)
        {
            return context.Set<Role>().FirstOrDefault(role => role.Name == roleName)?.Users.Select(user => user.ToDalUser());
        }

        public void AddUserToRole(int userId, string roleName)
        {
            User user = context.Set<User>().FirstOrDefault(u => u.Id == userId);
            if (user == null) return;

            Role role = context.Set<Role>().FirstOrDefault(r => r.Name == roleName);
            if (role == null) return;

            role.Users.Add(user);
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
