using System.Collections.Generic;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IRatingService : IService<RatingEntity>
    {
        IEnumerable<RatingEntity> GetPhotoRatings(int photoId);
        RatingEntity GetUserRatingOfPhoto(int userId, int photoId);
    }
}
