using System.Collections.Generic;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        IEnumerable<DalRole> GetUserRoles(int userId);
        IEnumerable<DalUser> GetUsersInRole(string roleNane);
        void AddUserToRole(int userId, string roleName);
    }
}
