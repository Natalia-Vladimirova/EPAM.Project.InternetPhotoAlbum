using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork uow;
        private readonly IRatingRepository repository;

        public RatingService(IUnitOfWork uow, IRatingRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public void CreateEntity(RatingEntity rating)
        {
            repository.Create(rating.ToDalRating());
            uow.Commit();
        }

        public void DeleteEntity(RatingEntity rating)
        {
            repository.Delete(rating.ToDalRating());
            uow.Commit();
        }

        public IEnumerable<RatingEntity> GetPhotoRatings(int photoId)
        {
            return repository.GetPhotoRatings(photoId)?.Select(rating => rating.ToBllRating());
        }

        public RatingEntity GetEntity(int id)
        {
            return repository.GetById(id).ToBllRating();
        }

        public RatingEntity GetUserRatingOfPhoto(int userId, int photoId)
        {
            return repository.GetUserRatingOfPhoto(userId, photoId).ToBllRating();
        }

        public void UpdateEntity(RatingEntity rating)
        {
            repository.Update(rating.ToDalRating());
            uow.Commit();
        }
    }
}
