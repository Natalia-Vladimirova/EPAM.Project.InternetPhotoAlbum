using System;
using System.Collections.Generic;
using System.Linq;
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

        public HomeController(IUserService userService)
        {
            this.userService = userService;
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

        
    }
}