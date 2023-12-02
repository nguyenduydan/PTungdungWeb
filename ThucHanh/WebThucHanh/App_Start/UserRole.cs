using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebThucHanh.App_Start
{
    public class UserRole : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filtercontext)
        {
            var user = SessionConfig.GetUser();
            if (user == null)
            {
                // Điều hướng về trang đăng nhập
                filtercontext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Admin",
                        action = "Dangnhap",
                        area = "Admin"
                    })) ;

                return ;
            }
            return;
        }
    }
}