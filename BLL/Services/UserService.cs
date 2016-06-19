using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository repository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public void CreateEntity(UserEntity user)
        {
            repository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteEntity(UserEntity user)
        {
            repository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return repository.GetAll().Select(user => user.ToBllUser());
        }

        public UserEntity GetEntity(int id)
        {
            return repository.GetById(id).ToBllUser();
        }

        public UserEntity GetUserEntityByLogin(string login)
        {
            return repository.GetByLogin(login).ToBllUser();
        }

        public void UpdateEntity(UserEntity user)
        {
            repository.Update(user.ToDalUser());
            uow.Commit();
        }

        public IEnumerable<UserEntity> GetUserEntitiesByFirstName(string firstName)
        {
            return repository.GetUsersByFirstName(firstName).Select(u => u.ToBllUser());
        }

        public IEnumerable<UserEntity> GetUserEntitiesByLastName(string lastName)
        {
            return repository.GetUsersByLastName(lastName).Select(u => u.ToBllUser());
        }

        public void ChangeUserPassword(string login, string password)
        {
            repository.ChangeUserPassword(login, password);
            uow.Commit();
        }
    }
}
