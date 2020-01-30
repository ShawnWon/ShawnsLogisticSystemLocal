using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ADprojectteam1.Filter
{
    public class StoreClerkFilter: ActionFilterAttribute, IAuthorizationFilter{
        public void OnAuthorization(AuthorizationContext ac)
        {
            string sessionRole = (string)HttpContext.Current.Session["sessionRole"];

            if (!IsValidSessionRole(sessionRole))
                ac.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                        {"controller","Home" },
                        {"action","Login" }
                    });

        }

        public bool IsValidSessionRole(string sessionRole)
        {

            if (sessionRole == null)
                return false;
            if (!sessionRole.Equals("StoreClerk"))
                return false;
            return true;
        }
    }
}