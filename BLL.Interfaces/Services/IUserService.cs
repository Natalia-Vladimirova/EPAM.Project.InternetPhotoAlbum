﻿using System.Collections.Generic;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity GetUserEntityByLogin(string login);
        void ChangeUserPassword(string login, string password);
        IEnumerable<UserEntity> GetAllUserEntities();
        IEnumerable<UserEntity> GetUserEntitiesByFirstName(string firstName);
        IEnumerable<UserEntity> GetUserEntitiesByLastName(string lastName);
    }
}
