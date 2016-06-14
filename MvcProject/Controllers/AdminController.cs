using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcProject.Infrastructure;
using MvcProject.Models;
using MvcProject.Mappers;
using BLL.Interfaces.Services;

namespace MvcProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public AdminController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public ActionResult UsersEdit()
        {
            IEnumerable<UserViewModel> allUsers = userService.GetAllUserEntities().Select(u => u.ToMvcUser());
            IEnumerable<UserViewModel> admins = roleService.GetUserEntitiesInRole("admin").Select(u => u.ToMvcUser());
            IEnumerable<UserViewModel> users = allUsers.Except(admins, new UserIdComparer());
            return View(users);
        }

        [HttpGet]
        public ActionResult EditUser(int id = 0)
        {
            UserViewModel user = userService.GetUserEntity(id)?.ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("UsersEdit");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel viewModel)
        {
            UserViewModel user = userService.GetUserEntity(viewModel.Id)?.ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("UsersEdit");
            }

            if (ModelState.IsValid)
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.DateOfBirth = viewModel.DateOfBirth;
                userService.UpdateUser(user.ToBllUser());
                return RedirectToAction("UsersEdit");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id = 0)
        {
            UserViewModel user = userService.GetUserEntity(id)?.ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("UsersEdit");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(UserViewModel viewModel)
        {
            userService.DeleteUser(viewModel.ToBllUser());
            return RedirectToAction("UsersEdit");
        }

    }
}