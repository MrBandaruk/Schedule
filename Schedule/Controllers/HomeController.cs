using System.Web.Mvc;

namespace Schedule.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Description page";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }

        public ActionResult Feedback()
        {
            ViewBag.Message = "Feedback page.";

            return View();  
        }
  


    }
}