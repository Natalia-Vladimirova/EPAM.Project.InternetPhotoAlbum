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
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalUser dalUser)
        {
            context.Set<User>().Add(dalUser.ToOrmUser());
        }

        public void Delete(DalUser dalUser)
        {
            User user = context.Set<User>().FirstOrDefault(u => u.Id == dalUser.Id);
            if (user != null)
            {
                context.Set<User>().Remove(user);
            }
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(user => user.ToDalUser());
        }

        public DalUser GetById(int id)
        {
            return context.Set<User>().FirstOrDefault(user => user.Id == id)?.ToDalUser();
        }

        public DalUser GetByLogin(string login)
        {
            return context.Set<User>().FirstOrDefault(user => user.Login == login)?.ToDalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalUser dalUser)
        {
            User user = context.Set<User>().FirstOrDefault(u => u.Id == dalUser.Id);
            user.Login = dalUser.Login;
            user.Password = dalUser.Password;
            user.FirstName = dalUser.FirstName;
            user.LastName = dalUser.LastName;
            user.DateOfBirth = dalUser.DateOfBirth;
            user.UserPhoto = dalUser.UserPhoto;
        }
    }
}
