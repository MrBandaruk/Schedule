using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
            if (!ModelState.IsValid)
            {
                //bad request
                return new HttpStatusCodeResult(400, "All fields are required!"); //TODO: return Error message in little red window.
            }

            calDbProv.Add(item);
            return Json( new { success = true, item }, JsonRequestBehavior.AllowGet);
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

        #endregion





        #region Update

        [HttpGet]
        public ActionResult EditEvent(int id)
        {
            var item = calDbProv.GetById(id);
            return Json(new { item }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EditEvent(BLL.Model.CalendarModelItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(); //TODO: return Error message in little red window.
            }
            calDbProv.Update(item);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DragEditEvent(BLL.Model.CalendarViewModelItem item)
        {
            if (!ModelState.IsValid)
            {
                return View(); //TODO: return Error message in little red window.
            }

            calDbProv.Update(item);
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        #endregion





        #region Delete

        public ActionResult DeleteEvent(int id)
        {
            calDbProv.Delete(id); //TODO: return Error message in a little green window.

            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        #endregion






    }
}