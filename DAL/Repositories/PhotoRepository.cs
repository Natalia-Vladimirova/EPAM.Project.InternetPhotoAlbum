using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces.DataTransferObjects;
using DAL.Interfaces.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DbContext context;

        public PhotoRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalPhoto entity)
        {
            context.Set<Photo>().Add(entity.ToOrmPhoto());
        }

        public void Delete(DalPhoto entity)
        {
            Photo photo = context.Set<Photo>().FirstOrDefault(ph => ph.PhotoId == entity.Id);
            if (photo != null)
            {
                context.Set<Photo>().Remove(photo);
            }
        }

        public IEnumerable<DalPhoto> GetAll()
        {
            return context.Set<Photo>().Select(photo => photo.ToDalPhoto());
        }

        public DalPhoto GetById(int id)
        {
            return context.Set<Photo>().FirstOrDefault(photo => photo.PhotoId == id)?.ToDalPhoto();
        }

        public DalPhoto GetByPredicate(Expression<Func<DalPhoto, bool>> f)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalPhoto> GetUserPhotos(int userId)
        {
            return context.Set<User>().FirstOrDefault(user => user.Id == userId)?.Photos.Select(photo => photo.ToDalPhoto());
        }

        public void Update(DalPhoto entity)
        {
            Photo photo = context.Set<Photo>().FirstOrDefault(ph => ph.PhotoId == entity.Id);

            if (photo != null)
            {
                photo.Name = entity.Name;
                photo.Description = entity.Description;
            }
        }
    }
}
