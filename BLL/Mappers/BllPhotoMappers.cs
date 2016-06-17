using DAL.Interfaces.DataTransferObjects;
using BLL.Interfaces.Entities;

namespace BLL.Mappers
{
    public static class BllPhotoMappers
    {
        public static DalPhoto ToDalPhoto(this PhotoEntity photoEntity)
        {
            if (photoEntity == null) return null;

            return new DalPhoto()
            {
                Id = photoEntity.Id,
                Name = photoEntity.Name,
                Description = photoEntity.Description,
                Image = photoEntity.Image,
                TotalRate = photoEntity.TotalRate,
                CreationDate = photoEntity.CreationDate,
                UserId = photoEntity.UserId
            };
        }

        public static PhotoEntity ToBllPhoto(this DalPhoto dalPhoto)
        {
            if (dalPhoto == null) return null;

            return new PhotoEntity()
            {
                Id = dalPhoto.Id,
                Name = dalPhoto.Name,
                Description = dalPhoto.Description,
                Image = dalPhoto.Image,
                TotalRate = dalPhoto.TotalRate,
                CreationDate = dalPhoto.CreationDate,
                UserId = dalPhoto.UserId
            };
        }
    }
}
