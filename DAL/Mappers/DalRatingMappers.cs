using ORM;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Mappers
{
    public static class DalRatingMappers
    {
        public static Rating ToBllRating(this DalRating dalRating)
        {
            return new Rating()
            {
                RatingId = dalRating.Id,
                UserId = dalRating.UserId,
                PhotoId = dalRating.PhotoId,
                UserRate = dalRating.UserRate
            };
        }

        public static DalRating ToBllRating(this Rating rating)
        {
            return new DalRating()
            {
                Id = rating.RatingId,
                UserId = rating.UserId,
                PhotoId = rating.PhotoId,
                UserRate = rating.UserRate
            };
        }
    }
}
