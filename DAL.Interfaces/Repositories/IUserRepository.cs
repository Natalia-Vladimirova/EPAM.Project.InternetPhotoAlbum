using System.Collections.Generic;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByLogin(string login);
        void ChangeUserPassword(string login, string password);
        IEnumerable<DalUser> GetAll();
        IEnumerable<DalUser> GetUsersByFirstName(string firstName);
        IEnumerable<DalUser> GetUsersByLastName(string lastName);
    }
}
