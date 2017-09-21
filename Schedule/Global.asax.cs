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

        protected void Application_Error(Object sender, EventArgs e)
        {

            ////Response.Redirect("/Error/Http404"); //работает простой редирект
            //var exception = Server.GetLastError();

            ////log exception
            //log.Error(exception);

            //Response.Clear();
            //var httpException = exception as HttpException;

            //var routeData = new RouteData();
            //routeData.Values.Add("controller", "Error");


            //if (httpException != null)
            //{
            //    Response.StatusCode = httpException.GetHttpCode();

            //    switch (Response.StatusCode)
            //    {
            //        case 500:
            //            routeData.Values.Add("action", "ServerError");
            //            break;
            //        case 404:
            //            routeData.Values.Add("action", "NotFound");
            //            break;
            //        default:
            //            routeData.Values.Add("action", "General");
            //            routeData.Values.Add("exception", exception);
            //            break;
            //    }

            //}
            //else
            //{
            //    routeData.Values.Add("action", "General");
            //    routeData.Values.Add("exception", exception);
            //}

            //Server.ClearError();
            //Response.TrySkipIisCustomErrors = true;
            //IController errorsController = new ErrorController();
            //errorsController.Execute(new RequestContext(
            //    new HttpContextWrapper(Context), routeData));

            var httpContext = ((MvcApplication)sender).Context;
            var currentController = string.Empty;
            var currentAction = string.Empty;
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                if (!string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (!string.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            
            var ex = Server.GetLastError();

            
            log.Error(ex.Message);
            
            var controller = new ErrorController();
            var routeData = new RouteData();           
            var action = "General";
            

            
            if (ex is HttpException)
            {
                switch (((HttpException)ex).GetHttpCode())
                {
                    case 500:
                        action = "ServerError";
                        routeData.Values.Add("action", action);
                        break;
                    case 404:
                        action = "NotFound";
                        routeData.Values.Add("action", action);
                        break;
                    default:
                        action = "General";
                        routeData.Values.Add("action", action);
                        break;
                        
                }
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values.Add("controller", "Error");
            routeData.Values["action"] = action;
            Response.ContentType = "text/html";
            controller.ViewData.Model = new Exception(ex.ToString());
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));

        }
    }
}
