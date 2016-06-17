using System.Web.Mvc;
using MvcProject.Infrastructure.Mappers;
using MvcProject.Models;
using BLL.Interfaces.Services;

namespace MvcProject.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IUserService userService;
        private readonly IRatingService ratingService;

        public RatingController(IUserService userService, IRatingService ratingService)
        {
            this.userService = userService;
            this.ratingService = ratingService;
        }

        public ActionResult Rate(string userName, int photoId, int rating, string photoName, int page = 1)
        {
            UserViewModel currentUser = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            RatingViewModel userRating = ratingService.GetUserRatingOfPhoto(currentUser.Id, photoId).ToMvcRating();

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
            return RedirectToAction("Photos", "Photo", 
                new { userName = userName, page = page, currentPhotoId = photoId, photoName = photoName });
        }

        public ActionResult RemoveRate(string userName, int photoId, string photoName, int page = 1)
        {
            UserViewModel currentUser = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            RatingViewModel userRating = ratingService.GetUserRatingOfPhoto(currentUser.Id, photoId).ToMvcRating();

            if (userRating != null)
            {
                ratingService.DeleteEntity(userRating.ToBllRating());
            }
            return RedirectToAction("Photos", "Photo", 
                new { userName = userName, page = page, currentPhotoId = photoId, photoName = photoName });
        }

    }
}