using ORM;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Mappers
{
    public static class DalRatingMappers
    {
        public static Rating ToOrmRating(this DalRating dalRating)
        {
            if (dalRating == null) return null;

            return new Rating()
            {
                RatingId = dalRating.Id,
                UserId = dalRating.UserId,
                PhotoId = dalRating.PhotoId,
                UserRate = dalRating.UserRate
            };
        }

        public static DalRating ToDalRating(this Rating rating)
        {
            if (rating == null) return null;

            return new DalRating()
            {
                Id = rating.RatingId,
                UserId = rating.UserId ?? 0,
                PhotoId = rating.PhotoId,
                UserRate = rating.UserRate
            };
        }
    }
}
