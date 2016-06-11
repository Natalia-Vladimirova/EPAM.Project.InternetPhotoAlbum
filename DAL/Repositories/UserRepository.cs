using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DataTransferObjects;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void Create(DalUser e)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalUser e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalUser GetById(int key)
        {
            throw new NotImplementedException();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
