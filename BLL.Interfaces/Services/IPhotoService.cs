using System.Collections.Generic;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IPhotoService
    {
        PhotoEntity GetPhotoEntity(int id);
        IEnumerable<PhotoEntity> GetUserPhotos(int userId);
        void CreatePhoto(PhotoEntity photo);
        void UpdatePhoto(PhotoEntity photo);
        void DeletePhoto(PhotoEntity photo);
    }
}
