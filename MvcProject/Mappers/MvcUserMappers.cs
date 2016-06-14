using MvcProject.Models;
using BLL.Interfaces.Entities;

namespace MvcProject.Mappers
{
    public static class MvcUserMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
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