using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schedule.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CreateEvent(BLL.Model.CalendarModel item)
        {
            var al = item;
            var gg = al;
            return RedirectToAction("Index", "Calendar");
        }

        public ActionResult FullIndex()
        {
            return View();
        }
    }
}