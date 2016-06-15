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
            UserViewModel user = userService.GetEntity(id)?.ToMvcUser();

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
                userService.UpdateEntity(user.ToBllUser());

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Photos(string userName, int page = 1, int currentPhotoId = 0)
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
            var ratings = ratingService.GetPhotoRatings(currentPhotoId)?.Select(r =>
            new RatingViewModel
            {
                Id = r.Id,
                UserRate = r.UserRate,
                PhotoId = r.PhotoId,
                UserId = r.UserId,
                User = userService.GetEntity(r.UserId)?.ToMvcUser()
            });

            int pageSize = 4;
            var photosPerCurrentPage = photos.Skip((page - 1) * pageSize).Take(pageSize);

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = photos.Count()
            };

            PhotosViewModel photosModel = new PhotosViewModel
            {
                ChosenUser = user,
                Photos = photosPerCurrentPage,//photos,
                CurrentPhoto = photos?.FirstOrDefault(ph => ph.Id == currentPhotoId),
                CurrentPhotoRatings = ratings,
                RatingOfCurrentUser = ratingService.GetUserRatingOfPhoto(currentUser.Id, currentPhotoId)?.UserRate,
                PageInfo = pageInfo
            };
            return View(photosModel);
        }

        public ActionResult SearchPhotos(string photoName, string userName, int currentPhotoId = 0)
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

            var photos = photoService.GetUserPhotos(user.Id).Where(ph => ph.Name.Contains(photoName.Trim())).Select(ph => ph.ToMvcPhoto());
            var ratings = ratingService.GetPhotoRatings(currentPhotoId)?.Select(r =>
            new RatingViewModel
            {
                Id = r.Id,
                UserRate = r.UserRate,
                PhotoId = r.PhotoId,
                UserId = r.UserId,
                User = userService.GetEntity(r.UserId)?.ToMvcUser()
            });

            PhotosViewModel photosModel = new PhotosViewModel()
            {
                ChosenUser = user,
                Photos = photos,
                CurrentPhoto = photos?.FirstOrDefault(ph => ph.Id == currentPhotoId),
                CurrentPhotoRatings = ratings,
                RatingOfCurrentUser = ratingService.GetUserRatingOfPhoto(currentUser.Id, currentPhotoId)?.UserRate
            };
            return View("Photos", photosModel);
        }

        [HttpGet]
        public ActionResult AddPhoto(int page = 1)
        {
            ViewBag.CurrentPage = page;
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(PhotoViewModel viewModel, HttpPostedFileBase uploadImage, int page = 1)
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

                return RedirectToAction("Photos", new { page = page });
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult EditPhoto(int id = 0, int page = 1)
        {
            PhotoViewModel photo = photoService.GetEntity(id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }
            ViewBag.CurrentPage = page;
            return View(photo);
        }

        [HttpPost]
        public ActionResult EditPhoto(PhotoViewModel viewModel, int page = 1)
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
                return RedirectToAction("Photos", new { userName = User.Identity.Name, page = page, currentPhotoId = photo.Id });
            }
            return View(photo);
        }

        [HttpGet]
        public ActionResult DeletePhoto(int id = 0, int page = 1)
        {
            PhotoViewModel photo = photoService.GetEntity(id).ToMvcPhoto();

            if (photo == null)
            {
                return RedirectToAction("Photos");
            }
            ViewBag.CurrentPage = page;
            return View(photo);
        }

        [HttpPost]
        public ActionResult DeletePhoto(PhotoViewModel viewModel, int page = 1)
        {
            photoService.DeleteEntity(viewModel.ToBllPhoto());
            return RedirectToAction("Photos", new { page = page });
        }

        public ActionResult Rate(string userName, int photoId, int rating, int page = 1)
        {
            UserViewModel currentUser = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            RatingViewModel userRating = ratingService.GetUserRatingOfPhoto(currentUser.Id, photoId)?.ToMvcRating();

            if (userRating == null)
            {
                userRating = new RatingViewModel
                {
                    PhotoId = photoId,
                    UserId = currentUser.Id,
                    UserRate = rating
                };
                ratingService.CreateEntity(userRating.ToBllRating());
            }
            else
            {
                userRating.UserRate = rating;
                ratingService.UpdateEntity(userRating.ToBllRating());
            }
            return RedirectToAction($"Photos/{userName}/{page}/{photoId}");
        }

        public ActionResult RemoveRate(string userName, int photoId, int page = 1)
        {
            UserViewModel currentUser = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            RatingViewModel userRating = ratingService.GetUserRatingOfPhoto(currentUser.Id, photoId)?.ToMvcRating();

            if (userRating != null)
            {
                ratingService.DeleteEntity(userRating.ToBllRating());
            }
            return RedirectToAction($"Photos/{userName}/{page}/{photoId}");
        }

        [HttpGet]
        public ActionResult SearchUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchUsers(string firstName, string lastName)
        {
            IEnumerable<UserViewModel> foundUsers;

            if (firstName.Trim() == string.Empty && lastName.Trim() == string.Empty)
            {
                foundUsers = userService.GetAllUserEntities().Where(u => u.Login != User.Identity.Name).Select(u => u.ToMvcUser());
            }
            else if (firstName.Trim() == string.Empty)
            {
                foundUsers = userService.GetAllUserEntities()
                    .Where(u => u.Login != User.Identity.Name && u.LastName.Contains(lastName.Trim()))
                    .Select(u => u.ToMvcUser());
            }
            else if (lastName.Trim() == string.Empty)
            {
                foundUsers = userService.GetAllUserEntities()
                    .Where(u => u.Login != User.Identity.Name && u.FirstName.Contains(firstName.Trim()))
                    .Select(u => u.ToMvcUser());
            }
            else
            {
                foundUsers = userService.GetAllUserEntities()
                    .Where(u => u.Login != User.Identity.Name && u.FirstName.Contains(firstName.Trim()) &&
                                    u.LastName.Contains(lastName.Trim()))
                    .Select(u => u.ToMvcUser());
            }
            return View(foundUsers);
        }

    }
}