using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MvcProject.Models;
using MvcProject.Mappers;
using BLL.Interfaces.Services;

namespace MvcProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IPhotoService photoService;
        private readonly IRatingService ratingService;

        public HomeController(IUserService userService, IPhotoService photoService, IRatingService ratingService)
        {
            this.userService = userService;
            this.photoService = photoService;
            this.ratingService = ratingService;
        }

        public ActionResult Index(int id = 0)
        {
            UserViewModel user = userService.GetUserEntity(id)?.ToMvcUser();

            if (user == null)
            {
                user = userService.GetUserEntityByLogin(User.Identity.Name)?.ToMvcUser();
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult UserSettings()
        {
            UserViewModel user = userService.GetUserEntityByLogin(User.Identity.Name)?.ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult UserSettings(UserViewModel viewModel, HttpPostedFileBase uploadImage, string removePhoto)
        {
            UserViewModel user = userService.GetUserEntityByLogin(User.Identity.Name)?.ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.DateOfBirth = viewModel.DateOfBirth;

                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    user.UserPhoto = imageData;
                }

                if (removePhoto != null)
                {
                    user.UserPhoto = null;
                }
                userService.UpdateUser(user.ToBllUser());

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Photos(string userName, int currentPhotoId = 0)
        {
            UserViewModel currentUser = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            UserViewModel user = null;

            if (userName != null)
            {
                user = userService.GetUserEntityByLogin(userName)?.ToMvcUser();
            }

            if (user == null)
            {
                user = currentUser;
            }

            var photos = photoService.GetUserPhotos(user.Id).Select(ph => ph.ToMvcPhoto());
            var rating = ratingService.GetPhotoRatings(currentPhotoId)?.Select(r =>
            new RatingViewModel
            {
                RatingId = r.Id,
                UserRate = r.UserRate,
                PhotoId = r.PhotoId,
                UserId = r.UserId,
                User = userService.GetUserEntity(r.UserId)?.ToMvcUser()
            }
            );

            PhotosViewModel photosModel = new PhotosViewModel
            {
                ChosenUser = user,
                Photos = photos,
                CurrentPhoto = photos?.FirstOrDefault(ph => ph.Id == currentPhotoId),
                CurrentPhotoRatings = rating,
                RatingOfCurrentUser = ratingService.GetUserRatingOfPhoto(currentUser.Id, currentPhotoId)?.UserRate
            };

            return View(photosModel);
        }

        [HttpGet]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(PhotoViewModel viewModel, HttpPostedFileBase uploadImage)
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
                photoService.CreatePhoto(viewModel.ToBllPhoto());

                return RedirectToAction("Photos");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult EditPhoto(int id = 0)
        {
            PhotoViewModel photo = photoService.GetPhotoEntity(id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }

            return View(photo);
        }

        [HttpPost]
        public ActionResult EditPhoto(PhotoViewModel viewModel)
        {
            PhotoViewModel photo = photoService.GetPhotoEntity(viewModel.Id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }

            if (ModelState.IsValid)
            {
                photo.Name = viewModel.Name;
                photo.Description = viewModel.Description;
                photoService.UpdatePhoto(photo.ToBllPhoto());
                return RedirectToAction($"Photos/{User.Identity.Name}/{photo.Id}");
            }
            return View(photo);
        }

        [HttpGet]
        public ActionResult DeletePhoto(int id = 0)
        {
            PhotoViewModel photo = photoService.GetPhotoEntity(id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }

            return View(photo);
        }

        [HttpPost]
        public ActionResult DeletePhoto(PhotoViewModel viewModel)
        {
            photoService.DeletePhoto(viewModel.ToBllPhoto());
            return RedirectToAction("Photos");
        }

    }
}