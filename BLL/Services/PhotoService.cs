﻿using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork uow;
        private readonly IPhotoRepository repository;

        public PhotoService(IUnitOfWork uow, IPhotoRepository repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public void CreateEntity(PhotoEntity photo)
        {
            repository.Create(photo.ToDalPhoto());
            uow.Commit();
        }

        public void DeleteEntity(PhotoEntity photo)
        {
            repository.Delete(photo.ToDalPhoto());
            uow.Commit();
        }

        public PhotoEntity GetEntity(int id)
        {
            return repository.GetById(id).ToBllPhoto();
        }

        public IEnumerable<PhotoEntity> GetUserPhotos(int userId)
        {
            return repository.GetUserPhotos(userId).Select(photo => photo.ToBllPhoto());
        }

        public IEnumerable<PhotoEntity> GetUserPhotosByName(int userId, string photoName)
        {
            return repository.GetUserPhotosByName(userId, photoName).Select(photo => photo.ToBllPhoto());
        }

        public void UpdateEntity(PhotoEntity photo)
        {
            repository.Update(photo.ToDalPhoto());
            uow.Commit();
        }
    }
}
