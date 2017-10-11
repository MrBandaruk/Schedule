using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Schedule.BLL.Model;
using System.Security.Claims;
using Schedule.BLL.Interfaces;
using Schedule.BLL.Infrastructure;
using Schedule.DAL.Dto;

namespace Schedule.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Invalid login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Age = model.Age,
                    Name = model.Name,
                    Surname = model.Surname,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Login", "Account"); //add green little window with successful registration
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "nbandaruk@gmail.com",
                UserName = "admin",
                Password = "321",
                Name = "Nikita",
                Surname = "Bandaruk",
                Age = 17,

                Role = "admin",
            }, new List<string> { "user", "admin" });
        }

        [ChildActionOnly]
        public ActionResult HeaderLogin()
        {
            var model = new LoginModel();
            return PartialView("", model);
        }
    }
}