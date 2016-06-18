using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MvcProject.Infrastructure;
using MvcProject.Infrastructure.Mappers;
using MvcProject.Models;
using BLL.Interfaces.Services;

namespace MvcProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        private readonly IPhotoService photoService;

        public HomeController(IUserService userService, IPhotoService photoService)
        {
            this.userService = userService;
            this.photoService = photoService;
        }

        public ActionResult Index(int id = 0)
        {
            UserViewModel user = userService.GetEntity(id).ToMvcUser();

            if (user == null)
            {
                user = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            }
            int lastPhotosCount = 6;
            ViewBag.LastPhotos = photoService.GetUserPhotos(user.Id).Take(lastPhotosCount).Select(ph => ph.ToMvcPhoto());
            return View(user);
        }

        [HttpGet]
        public ActionResult UserSettings()
        {
            UserViewModel user = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult UserSettings(UserViewModel viewModel, HttpPostedFileBase uploadImage, string removePhoto)
        {
            UserViewModel user = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();

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

        [HttpGet]
        public ActionResult SearchUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchUsers(string firstName, string lastName)
        {
            var usersByFirstName = userService.GetUserEntitiesByFirstName(firstName).Select(u => u.ToMvcUser());
            var usersByLastName = userService.GetUserEntitiesByLastName(lastName).Select(u => u.ToMvcUser());

            IEnumerable<UserViewModel> foundUsers = usersByFirstName
                .Intersect(usersByLastName, new UserIdComparer())
                .Where(u => u.UserName != User.Identity.Name);

            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            return View(foundUsers);
        }

    }
}