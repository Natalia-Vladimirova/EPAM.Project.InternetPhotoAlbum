using System.Collections.Generic;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Interfaces.Repositories
{
    public interface IRatingRepository : IRepository<DalRating>
    {
        IEnumerable<DalRating> GetPhotoRatings(int photoId);
        DalRating GetUserRatingOfPhoto(int userId, int photoId);
    }
}
