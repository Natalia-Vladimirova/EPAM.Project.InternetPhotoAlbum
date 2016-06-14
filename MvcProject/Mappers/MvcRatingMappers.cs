using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcProject.Models;
using BLL.Interfaces.Entities;

namespace MvcProject.Mappers
{
    public static class MvcRatingMappers
    {
        public static RatingViewModel ToMvcRating(this RatingEntity ratingEntity)
        {
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