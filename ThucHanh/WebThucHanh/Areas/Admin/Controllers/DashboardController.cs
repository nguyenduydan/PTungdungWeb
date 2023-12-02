using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucHanh.App_Start;

namespace WebThucHanh.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        [UserRole]
        public ActionResult Index()
        {
            return View();
        }
    }
}