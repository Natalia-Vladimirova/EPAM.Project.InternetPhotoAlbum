using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcProject.Infrastructure;
using MvcProject.Infrastructure.Mappers;
using MvcProject.Models;
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

        public ActionResult UsersEdit(int page = 1)
        {
            IEnumerable<UserViewModel> allUsers = userService.GetAllUserEntities().Select(u => u.ToMvcUser());
            IEnumerable<UserViewModel> admins = roleService.GetUserEntitiesInRole("admin").Select(u => u.ToMvcUser());
            IEnumerable<UserViewModel> users = allUsers.Except(admins, new UserIdComparer());

            int pageSize = 4;
            var usersPerCurrentPage = users.Skip((page - 1) * pageSize).Take(pageSize);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_UsersEdit", usersPerCurrentPage);
            }

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = users.Count()
            };

            UsersViewModel viewModel = new UsersViewModel
            {
                Users = usersPerCurrentPage,
                PageInfo = pageInfo
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult EditUser(int id = 0)
        {
            UserViewModel user = userService.GetEntity(id).ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("UsersEdit");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel viewModel)
        {
            UserViewModel user = userService.GetEntity(viewModel.Id).ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("UsersEdit");
            }

            if (ModelState.IsValid)
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.DateOfBirth = viewModel.DateOfBirth;
                userService.UpdateEntity(user.ToBllUser());
                return RedirectToAction("UsersEdit");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id = 0)
        {
            UserViewModel user = userService.GetEntity(id).ToMvcUser();

            if (user == null)
            {
                return RedirectToAction("UsersEdit");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(UserViewModel viewModel)
        {
            userService.DeleteEntity(viewModel.ToBllUser());
            return RedirectToAction("UsersEdit");
        }

    }
}