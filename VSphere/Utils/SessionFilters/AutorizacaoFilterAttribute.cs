using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace VSphere.Utils.SessionFilters
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    object user = context.HttpContext.Session.Set("UserSingUp", user);

        //    if (user == null)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
        //        {
        //            controller = "login",
        //            action = "Index"
        //        }));
        //    }
        //}
    }
}
