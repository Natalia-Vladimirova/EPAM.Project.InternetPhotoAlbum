using System.Web.Mvc;
using System.Web.Security;
using MvcProject.Infrastructure.Mappers;
using MvcProject.Infrastructure.Providers;
using MvcProject.Models;
using BLL.Interfaces.Services;

namespace MvcProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userSerivce)
        {
            this.userService = userSerivce;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            var user = userService.GetUserEntityByLogin(viewModel.Login);

            if (user != null)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Login, viewModel.Password);

                if (membershipUser != null)
                {
                    var userEnitity = userService.GetUserEntityByLogin(viewModel.Login);
                    userEnitity.FirstName = viewModel.FirstName;
                    userEnitity.LastName = viewModel.LastName;
                    userEnitity.DateOfBirth = viewModel.DateOfBirth;
                    userService.UpdateEntity(userEnitity);

                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);

                    var temp = User.IsInRole("user");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Login, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, false);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
                
            }

            ViewBag.returnUrl = returnUrl;
            return View(viewModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(PasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool passwordIsChanged = ((CustomMembershipProvider)Membership.Provider)
                    .ChangePassword(User.Identity.Name, viewModel.OldPassword, viewModel.NewPassword);

                if (passwordIsChanged)
                {
                    return RedirectToAction("UserSettings", "Home");
                }
                ModelState.AddModelError("OldPassword", "Incorrect password.");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult DeleteAccount()
        {
            var user = userService.GetUserEntityByLogin(User.Identity.Name).ToMvcUser();
            return View(user);
        }

        [HttpPost, ActionName("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccountConfirmed()
        {
            var user = userService.GetUserEntityByLogin(User.Identity.Name);
            FormsAuthentication.SignOut();
            userService.DeleteEntity(user);
            return RedirectToAction("Login");
        }

    }
}