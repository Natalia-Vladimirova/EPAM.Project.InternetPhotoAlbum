using System.Collections.Generic;
using BLL.Interfaces.Entities;

namespace BLL.Interfaces.Services
{
    public interface IRatingService
    {
        RatingEntity GetRatingEntity(int id);
        IEnumerable<RatingEntity> GetPhotoRatings(int photoId);
        RatingEntity GetUserRatingOfPhoto(int userId, int photoId);
        void CreateRating(RatingEntity rating);
        void UpdateRating(RatingEntity rating);
        void DeleteRating(RatingEntity rating);
    }
}
