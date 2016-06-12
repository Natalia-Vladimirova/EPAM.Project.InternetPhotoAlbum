using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcProject.Models;
using BLL.Interfaces.Entities;

namespace MvcProject.Mappers
{
    public static class MvcUserMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                UserName = userEntity.Login,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                DateOfBirth = userEntity.DateOfBirth,
                UserPhoto = userEntity.UserPhoto
            };
        }
    }
}