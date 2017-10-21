using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Schedule.Attributes;
using Schedule.DAL;
using Schedule.DAL.Entities;
using Schedule.DAL.Identity;
using Schedule.DAL.Repositories;

namespace Schedule.Controllers
{
    public class AdminController : Controller
    {
        ApplicationUserManager userManager = new IdentityUnitOfWork(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString).UserManager;

        [AdminAuth]
        public ActionResult Index()
        {           
            return View(userManager.Users);
        }

        [AdminAuth]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Error!";
                    return RedirectToAction("Index");
                }
            }

            TempData["Error"] = "Error!";
            return RedirectToAction("Index");
        }
    }
}