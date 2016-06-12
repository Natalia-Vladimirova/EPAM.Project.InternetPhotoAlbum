using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f);
        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
