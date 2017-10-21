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
        public ViewResult General()
        {
            return View();
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }


        public ViewResult ServerError()
        {
            Response.StatusCode = 500;
            return View();
        }

    }
}