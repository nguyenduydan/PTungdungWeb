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
        public ActionResult DangNhap()//
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection field)
        {
            string username = field["username"];//Khai báo một biến có tên là username kiểu chuỗi. Biến này sẽ được sử dụng để lưu trữ giá trị của tên người dùng.
            string password = field["password"]; //Khai báo một biến có tên là password kiểu chuỗi. Biến này sẽ được sử dụng để lưu trữ giá trị của mật khẩu người dùng.


            Users user = usersDAO.getRow(username, "admin"); // Chỗ này "role" có thể thay bằng role thực tế của bạn

            if (user != null && user.Password == password)
            {
                // Đăng nhập thành công, có thể thực hiện các hành động khác ở đây
                SessionConfig.SetUser(user);
                // Ví dụ: Lưu thông tin người dùng vào session
                //session là một cơ chế được sử dụng để lưu trữ thông tin trạng thái của một phiên làm việc(session) của người dùng trên máy chủ
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
            Session.Abandon();
            return Redirect("https://localhost:44326/"); 
        }
    }
}