using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebLoggR.Filters
{
    public class RequireSessionAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            bool skipAuthorize =
                filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (!skipAuthorize)
            {
                if (session["AccountId"] == null)
                {
                    filterContext.Result = new RedirectResult("/Home/LogIn");
                }
            }
            base.OnAuthorization(filterContext);
        }
    }
}