using MvcProject.Models;
using BLL.Interfaces.Entities;

namespace MvcProject.Infrastructure.Mappers
{
    public static class MvcRatingMappers
    {
        public static RatingViewModel ToMvcRating(this RatingEntity ratingEntity)
        {
            if (ratingEntity == null) return null;

            return new RatingViewModel()
            {
                Id = ratingEntity.Id,
                UserRate = ratingEntity.UserRate,
                PhotoId = ratingEntity.PhotoId,
                UserId = ratingEntity.UserId
            };
        }

        public static RatingEntity ToBllRating(this RatingViewModel mvcRating)
        {
            if (mvcRating == null) return null;

            return new RatingEntity()
            {
                Id = mvcRating.Id,
                UserRate = mvcRating.UserRate,
                PhotoId = mvcRating.PhotoId,
                UserId = mvcRating.UserId
            };
        }
    }
}