using Schedule.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Schedule
{
    public class MvcApplication : System.Web.HttpApplication
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }

        protected void Application_Error()
        {
            //Exception exception = Server.GetLastError();
            //Server.ClearError();
            //Response.Redirect("/Error/Http404"); //работает простой редирект
            var exception = Server.GetLastError();
            
            //log exception
            log.Error(exception);

            Response.Clear();
            var httpException = exception as HttpException;

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");


            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();

                switch (Response.StatusCode)
                {
                    case 403:
                        routeData.Values.Add("action", "Http403");
                        break;
                    case 404:
                        routeData.Values.Add("action", "Http404");
                        break;
                    default:
                        routeData.Values.Add("action", "Error");
                        break;
                }

            }
            else
            {
                routeData.Values.Add("action", "Index");
            }

            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;
            IController errorsController = new ErrorController();
            errorsController.Execute(new RequestContext(
                new HttpContextWrapper(Context), routeData));

        }
    }
}
