using DAL.Interfaces.DataTransferObjects;
using BLL.Interfaces.Entities;

namespace BLL.Mappers
{
    public static class BllUserMappers
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Login = userEntity.Login,
                Password = userEntity.Password,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                DateOfBirth = userEntity.DateOfBirth,
                UserPhoto = userEntity.UserPhoto
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Login = dalUser.Login,
                Password = dalUser.Password,
                FirstName = dalUser.FirstName,
                LastName = dalUser.LastName,
                DateOfBirth = dalUser.DateOfBirth,
                UserPhoto = dalUser.UserPhoto
            };
        }
    }
}
