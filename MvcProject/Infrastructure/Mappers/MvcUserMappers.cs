using MvcProject.Models;
using BLL.Interfaces.Entities;

namespace MvcProject.Infrastructure.Mappers
{
    public static class MvcUserMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            if (userEntity == null) return null;

            return new UserViewModel()
            {
                Id = userEntity.Id,
                UserName = userEntity.Login,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                DateOfBirth = userEntity.DateOfBirth,
                UserPhoto = userEntity.UserPhoto
            };
        }

        public static UserEntity ToBllUser(this UserViewModel mvcUser)
        {
            if (mvcUser == null) return null;

            return new UserEntity()
            {
                Id = mvcUser.Id,
                Login = mvcUser.UserName,
                FirstName = mvcUser.FirstName,
                LastName = mvcUser.LastName,
                DateOfBirth = mvcUser.DateOfBirth,
                UserPhoto = mvcUser.UserPhoto
            };
        }
    }
}