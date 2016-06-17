using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.DataTransferObjects;
using DAL.Interfaces.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly DbContext context;

        public RatingRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalRating entity)
        {
            context.Set<Rating>().Add(entity.ToOrmRating());
        }

        public void Delete(DalRating entity)
        {
            Rating rating = context.Set<Rating>().FirstOrDefault(r => r.UserId == entity.UserId && r.PhotoId == entity.PhotoId);
            if (rating != null)
            {
                context.Set<Rating>().Remove(rating);
            }
        }
        
        public DalRating GetById(int id)
        {
            return context.Set<Rating>().FirstOrDefault(rating => rating.RatingId == id).ToDalRating();
        }
        
        public IEnumerable<DalRating> GetPhotoRatings(int photoId)
        {
            return context.Set<Photo>().FirstOrDefault(photo => photo.PhotoId == photoId)?.Ratings.Select(rating => rating.ToDalRating());
        }

        public DalRating GetUserRatingOfPhoto(int userId, int photoId)
        {
            return context.Set<Rating>().FirstOrDefault(r => r.UserId == userId && r.PhotoId == photoId).ToDalRating();
        }

        public void Update(DalRating entity)
        {
            Rating rating = context.Set<Rating>().FirstOrDefault(r => r.UserId == entity.UserId && r.PhotoId == entity.PhotoId);

            if (rating != null)
            {
                rating.UserRate = entity.UserRate;
            }
        }
    }
}
