using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        public void CreateUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
