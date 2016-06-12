using DAL.Interfaces.DataTransferObjects;

namespace DAL.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetByLogin(string login);
    }
}
