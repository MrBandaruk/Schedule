using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Schedule.BLL.Model;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Schedule.BLL.Interfaces;
using Schedule.BLL.Infrastructure;
using Schedule.DAL;
using Schedule.DAL.Dto;
using Schedule.DAL.Entities;
using Schedule.DAL.Identity;

namespace Schedule.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

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
                UserDto userDto = new UserDto { Email = model.Email, Password = model.Password };
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
                UserDto userDto = new UserDto
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
            await UserService.SetInitialData(new UserDto
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

        public ActionResult Profile()
        {
            //var model = User.Identity.GetUserId();
            //var manager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext()));
            //var userModel = manager.FindById(model);
            //var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
    }
}