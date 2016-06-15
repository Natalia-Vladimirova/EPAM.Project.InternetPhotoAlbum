using System;
using System.Collections.Generic;

namespace BLL.Interfaces.Services
{
    public interface IService<T>
    {
        T GetEntity(int id);
        void CreateEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
    }
}
