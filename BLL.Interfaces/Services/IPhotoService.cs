using System.Collections.Generic;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IPhotoService : IService<PhotoEntity>
    {
        IEnumerable<PhotoEntity> GetUserPhotos(int userId);
    }
}
