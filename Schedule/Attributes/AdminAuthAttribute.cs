using System.Web;
using System.Web.Mvc;

namespace Schedule.BLL.Attributes
{
    public class AdminAuthAttribute : AuthorizeAttribute
    {
        //public override void AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var authorized = base.AuthorizeCore(httpContext);
        //    if (!user.IsInRole("Admin"))
        //    {
        //        var res = new ViewResult() { ViewName = "AuthorizeFaild" };
        //    }

        //}
    }
}
