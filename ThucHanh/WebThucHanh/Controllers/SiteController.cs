using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebThucHanh.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        public ActionResult Index()
        {
            MyDBContext db = new MyDBContext();//tao moi db
            int sodong = db.Orders.Count();
            ViewBag.sodong =sodong;
            return View();
        }
    }
}