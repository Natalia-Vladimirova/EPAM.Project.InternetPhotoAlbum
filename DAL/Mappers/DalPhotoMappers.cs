using ORM;
using DAL.Interfaces.DataTransferObjects;

namespace DAL.Mappers
{
    public static class DalPhotoMappers
    {
        public static Photo ToOrmPhoto(this DalPhoto dalPhoto)
        {
            if (dalPhoto == null) return null;

            return new Photo()
            {
                PhotoId = dalPhoto.Id,
                Name = dalPhoto.Name,
                Description = dalPhoto.Description,
                Image = dalPhoto.Image,
                TotalRate = dalPhoto.TotalRate,
                CreationDate = dalPhoto.CreationDate,
                UserId = dalPhoto.UserId
            };
        }

        public static DalPhoto ToDalPhoto(this Photo photo)
        {
            if (photo == null) return null;

            return new DalPhoto()
            {
                Id = photo.PhotoId,
                Name = photo.Name,
                Description = photo.Description,
                Image = photo.Image,
                TotalRate = photo.TotalRate,
                CreationDate = photo.CreationDate,
                UserId = photo.UserId
            };
        }
    }
}
