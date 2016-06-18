using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        
        public DalPhoto GetById(int id)
        {
            return context.Set<Photo>().FirstOrDefault(photo => photo.PhotoId == id).ToDalPhoto();
        }
        
        public IEnumerable<DalPhoto> GetUserPhotos(int userId)
        {
            return context.Set<Photo>()
                .Where(ph => ph.UserId == userId)
                .OrderByDescending(ph => ph.CreationDate)
                .AsEnumerable()
                .Select(ph => ph.ToDalPhoto());
        }

        public IEnumerable<DalPhoto> GetUserPhotosByName(int userId, string photoName)
        {
            if (string.IsNullOrWhiteSpace(photoName))
            {
                return GetUserPhotos(userId);
            }
            photoName = photoName.Trim();
            return context.Set<Photo>()
                .Where(ph => ph.UserId == userId)
                .Where(ph => ph.Name.Contains(photoName))
                .OrderByDescending(ph => ph.CreationDate)
                .AsEnumerable()
                .Select(ph => ph.ToDalPhoto());
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
