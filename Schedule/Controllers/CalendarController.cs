using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schedule.Controllers
{
    public class CalendarController : Controller
    {
        public BLL.Providers.CalendarDbProvider calDbProv = new BLL.Providers.CalendarDbProvider();

        [HttpGet]
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult EventsData()
        {          
            return Json(new { calDbProv.GetAll().Events }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditEvent(int id)
        {
            var item = calDbProv.GetById(id);
            return Json(new { item }, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public ActionResult EditEvent(?? item)
        //{
        //    calDbProv.Update();
        //    return Json(new { ?? }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult CreateEvent(string title, string additional, DateTime startDate, DateTime endDate)
        {
            BLL.Model.CalendarModelItem item = new BLL.Model.CalendarModelItem {
                Title = title,
                Additional = additional,
                StartDate = startDate,
                EndDate = endDate
            };

            calDbProv.Add(item);


            return Json( new { status = "success", item }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FullIndex()
        {
            return View();
        }
    }
}