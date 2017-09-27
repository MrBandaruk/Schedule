using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Schedule.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Change(string lang)
        {
            //set lang
            if(lang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }

            //save lang to cookies
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = lang;
            Response.Cookies.Add(cookie);

            //get action and controller for redirecting back to the same page
            //var currentController = this.RouteData.Values["controller"];
            //var currentAction = this.RouteData.Values["action"];
            //return RedirectToAction(currentAction.ToString(), currentController.ToString());

            return RedirectToAction("Index", "Home");
        }
    }
}