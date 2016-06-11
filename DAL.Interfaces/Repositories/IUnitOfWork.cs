using System;

namespace DAL.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
