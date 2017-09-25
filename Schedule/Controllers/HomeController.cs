using System.Web.Mvc;

namespace Schedule.Controllers
{
    public class HomeController : Controller
    {
        public BLL.Providers.NewsDbProvider newsDbProv = new BLL.Providers.NewsDbProvider();


        public ActionResult Index()
        {
            var model = newsDbProv.GetThreeLast();            
            return View(model);
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