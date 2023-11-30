using MyClass.DAO;
using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucHanh.Library;

namespace WebThucHanh.Controllers
{
    public class KhachhangController : Controller
    {
        UsersDAO usersDAO = new UsersDAO();
        //////////////////////////////////////////////////////////////////////////
        // GET: Khachhang DangNhap
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
                // Ví dụ: Lưu thông tin người dùng vào session
                Session["UserID"] = user.ID;
                Session["UserName"] = user.UserName;
                return RedirectToAction("Index","Site"); // Chuyển hướng đến trang chính sau khi đăng nhập thành công
            }
            else
            {
                // Đăng nhập không thành công, có thể thông báo lỗi hoặc thực hiện các hành động khác
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View("DangNhap");
            }
        }

        //////////////////////////////////////////////////////////////////////////
        // GET: Khachhang DangKy
        public ActionResult DangKy()
        {
            return View();
        }
    }
}