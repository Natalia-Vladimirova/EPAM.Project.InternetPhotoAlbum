using System.Collections.Generic;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Interfaces.Repositories
{
    public interface IPhotoRepository : IRepository<DalPhoto>
    {
        IEnumerable<DalPhoto> GetUserPhotos(int userId);
    }
}
