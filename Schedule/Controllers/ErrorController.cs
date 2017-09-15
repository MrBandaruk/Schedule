using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schedule.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult General(Exception exception)
        {
            return View("Exception", exception);
        }

        public ActionResult Http404()
        {
            return View();
        }

        public ActionResult Http403()
        {
            return View();
        }

        //public ActionResult Exception()
        //{
        //    return View();
        //}
    }
}