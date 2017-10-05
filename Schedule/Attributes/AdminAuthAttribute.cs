using System.Web;
using System.Web.Mvc;

namespace Schedule.Attributes
{
    public class AdminAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                { "controller", "Account" }, { "action", "Login" }
                });
            }
            else if (!filterContext.HttpContext.User.IsInRole("admin"))
            {
                filterContext.Result = new ViewResult() { ViewName = "AuthorizeFaild" };
            }
        }
    }
}
