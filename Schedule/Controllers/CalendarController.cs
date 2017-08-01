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


        #region Create

        public ActionResult CreateEvent(BLL.Model.CalendarModelItem item)
        { 
            calDbProv.Add(item);

            return Json( new { status = "success", item }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Read

        [HttpGet]
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult EventsData()
        {          
            return Json(new { calDbProv.GetAll().Events }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WCIndex()
        {
            return View();
        }

        #endregion


        #region Update

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

        #endregion


        #region Delete

        public ActionResult DeleteEvent(int id)
        {
            calDbProv.Delete(id);

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        #endregion






    }
}