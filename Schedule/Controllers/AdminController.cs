using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Schedule.Attributes;
using Schedule.DAL;
using Schedule.DAL.Identity;
using Schedule.DAL.Repositories;

namespace Schedule.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [AdminAuth]
        public ActionResult Index()
        {
            IdentityUnitOfWork man = new IdentityUnitOfWork(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            
            return View(man.UserManager.Users);
        }
    }
}