﻿using MvcProject.Models;
using BLL.Interfaces.Entities;

namespace MvcProject.Mappers
{
    public static class MvcPhotoMappers
    {
        public static PhotoViewModel ToMvcPhoto(this PhotoEntity photoEntity)
        {
            return new PhotoViewModel()
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

        public static PhotoEntity ToBllPhoto(this PhotoViewModel mvcPhoto)
        {
            return new PhotoEntity()
            {
                Id = mvcPhoto.Id,
                Name = mvcPhoto.Name,
                Description = mvcPhoto.Description,
                Image = mvcPhoto.Image,
                TotalRate = mvcPhoto.TotalRate,
                CreationDate = mvcPhoto.CreationDate,
                UserId = mvcPhoto.UserId
            };
        }
    }
}