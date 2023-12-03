using MyClass.DAO;
using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucHanh.App_Start;

namespace WebThucHanh.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        UsersDAO usersDAO = new UsersDAO();
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection field)
        {
            string username = field["username"];
            string password = field["password"];


            Users user = usersDAO.getRow(username, "admin"); // Chỗ này "role" có thể thay bằng role thực tế của bạn

            if (user != null && user.Password == password)
            {
                // Đăng nhập thành công, có thể thực hiện các hành động khác ở đây
                SessionConfig.SetUser(user);
                // Ví dụ: Lưu thông tin người dùng vào session
                Session["UserID"] = user.ID;
                Session["UserName"] = user.UserName;
                return RedirectToAction("Index", "Dashboard"); // Chuyển hướng đến trang chính sau khi đăng nhập thành công
            }
            else
            {
                // Đăng nhập không thành công, có thể thông báo lỗi hoặc thực hiện các hành động khác
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View("DangNhap");
            }
        }
        public ActionResult Dangxuat()
        {
            SessionConfig.SetUser(null);
            return RedirectToAction("Dangnhap", "Admin");
        }
        public ActionResult ReturnHome()
        {
            SessionConfig.SetUser(null);
            Session["UserId"] = null;
            Session["Username"] =null;
            return Redirect("http://fogvn29-001-site1.ctempurl.com/"); 
        }
    }
}