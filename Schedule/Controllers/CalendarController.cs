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


        public ActionResult CreateEvent(string title, string additional, DateTime startDate, DateTime endDate)
        {
            BLL.Model.CalendarModel item = new BLL.Model.CalendarModel {
                Title = title,
                Additional = additional,
                StartDate = startDate,
                EndDate = endDate
            };
            var al = item;
            var gg = al;
            return Json( new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FullIndex()
        {
            return View();
        }
    }
}