using System.Web.Mvc;
using System.Web.Security;
using MvcProject.Infrastructure.Providers;
using MvcProject.Models;
using BLL.Interfaces.Services;

namespace MvcProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userSerivce;

        public AccountController(IUserService userSerivce)
        {
            this.userSerivce = userSerivce;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            var user = userSerivce.GetUserEntityByLogin(viewModel.Login);

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
                    var userEnitity = userSerivce.GetUserEntityByLogin(viewModel.Login);
                    userEnitity.FirstName = viewModel.FirstName;
                    userEnitity.LastName = viewModel.LastName;
                    userEnitity.DateOfBirth = viewModel.DateOfBirth;
                    userSerivce.UpdateEntity(userEnitity);

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
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
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

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}