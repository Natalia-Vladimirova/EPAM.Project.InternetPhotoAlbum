using DAL.Interfaces.DataTransferObjects;
using BLL.Interfaces.Entities;

namespace BLL.Mappers
{
    public static class BllRatingMappers
    {
        public static DalRating ToDalRating(this RatingEntity ratingEntity)
        {
            if (ratingEntity == null) return null;

            return new DalRating()
            {
                Id = ratingEntity.Id,
                UserId = ratingEntity.UserId,
                PhotoId = ratingEntity.PhotoId,
                UserRate = ratingEntity.UserRate
            };
        }

        public static RatingEntity ToBllRating(this DalRating dalRating)
        {
            if (dalRating == null) return null;

            return new RatingEntity()
            {
                Id = dalRating.Id,
                UserId = dalRating.UserId,
                PhotoId = dalRating.PhotoId,
                UserRate = dalRating.UserRate
            };
        }
    }
}
