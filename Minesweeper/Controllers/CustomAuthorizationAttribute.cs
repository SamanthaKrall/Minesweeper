using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minesweeper.Controllers
{
    public class CustomAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            if (System.Web.HttpContext.Current.Session["Username"] == null)
                filterContext.Result = new RedirectResult("/User/Login");

        }
    }
}