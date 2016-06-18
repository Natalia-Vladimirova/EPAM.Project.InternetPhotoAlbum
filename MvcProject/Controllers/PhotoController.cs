using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MvcProject.Infrastructure.Mappers;
using MvcProject.Models;
using BLL.Interfaces.Services;

namespace MvcProject.Controllers
{
    [Authorize]
    public class PhotoController : Controller
    {
        private readonly IUserService userService;
        private readonly IPhotoService photoService;
        private readonly IRatingService ratingService;

        public PhotoController(IUserService userService, IPhotoService photoService, IRatingService ratingService)
        {
            this.userService = userService;
            this.photoService = photoService;
            this.ratingService = ratingService;
        }

        public ActionResult Photos(string userName, int page = 1, int currentPhotoId = 0, string photoName = null)
        {
            if (!string.IsNullOrWhiteSpace(photoName))
            {
                return RedirectToAction("SearchPhotos",
                    new
                    {
                        photoName = photoName,
                        userName = userName ?? User.Identity.Name,
                        page = page,
                        currentPhotoId = currentPhotoId
                    });
            }

            UserViewModel currentUser = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            UserViewModel user = null;

            if (userName != null)
            {
                user = userService.GetUserEntityByLogin(userName).ToMvcUser();
            }

            if (user == null)
            {
                user = currentUser;
            }
            var photos = photoService.GetUserPhotos(user.Id).Select(ph => ph.ToMvcPhoto());
            var photosModel = GetCurrentPhotosModel(user, photos, currentUser.Id, currentPhotoId, page);
            return View(photosModel);
        }

        public ActionResult SearchPhotos(string photoName, string userName, int page = 1, int currentPhotoId = 0)
        {
            UserViewModel currentUser = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            UserViewModel user = null;

            if (userName != null)
            {
                user = userService.GetUserEntityByLogin(userName).ToMvcUser();
            }

            if (user == null)
            {
                user = currentUser;
            }
            var photos = photoService.GetUserPhotosByName(user.Id, photoName).Select(ph => ph.ToMvcPhoto());
            var photosModel = GetCurrentPhotosModel(user, photos, currentUser.Id, currentPhotoId, page);
            ViewBag.PhotoName = photoName;
            return View("Photos", photosModel);
        }

        [HttpGet]
        public ActionResult AddPhoto(string photoName, int page = 1)
        {
            ViewBag.PhotoName = photoName;
            ViewBag.CurrentPage = page;
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(PhotoViewModel viewModel, HttpPostedFileBase uploadImage, string photoName, int page = 1)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage == null)
                {
                    ModelState.AddModelError("", "A photo is not selected.");
                    return View(viewModel);
                }

                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                viewModel.Image = imageData;
                viewModel.CreationDate = DateTime.Now;
                viewModel.UserId = userService.GetUserEntityByLogin(User.Identity.Name).Id;
                photoService.CreateEntity(viewModel.ToBllPhoto());

                return RedirectToAction("Photos", new { page = page, photoName = photoName });
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult EditPhoto(string photoName, int id = 0, int page = 1)
        {
            PhotoViewModel photo = photoService.GetEntity(id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }
            ViewBag.PhotoName = photoName;
            ViewBag.CurrentPage = page;
            return View(photo);
        }

        [HttpPost]
        public ActionResult EditPhoto(PhotoViewModel viewModel, string photoName, int page = 1)
        {
            PhotoViewModel photo = photoService.GetEntity(viewModel.Id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }

            if (ModelState.IsValid)
            {
                photo.Name = viewModel.Name;
                photo.Description = viewModel.Description;
                photoService.UpdateEntity(photo.ToBllPhoto());
                return RedirectToAction("Photos", 
                    new { userName = User.Identity.Name, page = page, currentPhotoId = photo.Id, photoName = photoName });
            }
            return View(photo);
        }

        [HttpGet]
        public ActionResult DeletePhoto(string photoName, int id = 0, int page = 1)
        {
            PhotoViewModel photo = photoService.GetEntity(id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }
            ViewBag.PhotoName = photoName;
            ViewBag.CurrentPage = page;
            return View(photo);
        }

        [HttpPost]
        public ActionResult DeletePhoto(PhotoViewModel viewModel, string photoName, int page = 1)
        {
            photoService.DeleteEntity(viewModel.ToBllPhoto());
            return RedirectToAction("Photos", new { page = page, photoName = photoName });
        }

        private PhotosViewModel GetCurrentPhotosModel(UserViewModel user, IEnumerable<PhotoViewModel> photos,
            int currentUserId, int currentPhotoId, int page)
        {
            var ratings = ratingService.GetPhotoRatings(currentPhotoId)?.Select(r =>
            new RatingViewModel
            {
                Id = r.Id,
                UserRate = r.UserRate,
                PhotoId = r.PhotoId,
                UserId = r.UserId,
                User = userService.GetEntity(r.UserId).ToMvcUser()
            });

            int pageSize = 4;
            var photosPerCurrentPage = photos.Skip((page - 1) * pageSize).Take(pageSize);

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = photos.Count()
            };

            return new PhotosViewModel
            {
                ChosenUser = user,
                Photos = photosPerCurrentPage,
                CurrentPhoto = photos?.FirstOrDefault(ph => ph.Id == currentPhotoId),
                CurrentPhotoRatings = ratings,
                RatingOfCurrentUser = ratingService.GetUserRatingOfPhoto(currentUserId, currentPhotoId)?.UserRate,
                PageInfo = pageInfo
            };
        }

    }
}