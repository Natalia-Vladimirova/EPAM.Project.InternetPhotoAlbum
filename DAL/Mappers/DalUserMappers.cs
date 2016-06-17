using ORM;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Mappers
{
    public static class DalUserMappers
    {
        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null) return null;

            return new User()
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

        public static DalUser ToDalUser(this User user)
        {
            if (user == null) return null;

            return new DalUser()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserPhoto = user.UserPhoto
            };
        }
    }
}
