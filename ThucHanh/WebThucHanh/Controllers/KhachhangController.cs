using MyClass.DAO;
using MyClass.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebThucHanh.App_Start;
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
            

            Users user = usersDAO.getRow(username, "customer"); // Chỗ này "role" có thể thay bằng role thực tế của bạn

            if (user != null && user.Password == password)
            {
                // Đăng nhập thành công, có thể thực hiện các hành động khác ở đây
                // Ví dụ: Lưu thông tin người dùng vào session
                Session["UserID"] = user.ID;
                Session["UserName"] = user.Fullname;
                return RedirectToAction("Index","Site"); // Chuyển hướng đến trang chính sau khi đăng nhập thành công
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
            Session["UserName"] = null;
            SessionConfig.SetUser(null);
            return RedirectToAction("Index", "Site");
        }

        //////////////////////////////////////////////////////////////////////////
        // GET: Khachhang DangKy
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            string mess = "";
            if (!string.IsNullOrEmpty(form["register"]))//nut ThemCategory duoc nhan
            {
                UsersDAO usersDAO = new UsersDAO();
                String fullname = form["fullname"];
                String email = form["email"];
                String phone = form["phone"];
                String username = form["username"];
                String password = form["password"];
                Users row_user = usersDAO.getRow(username, "customer");
                //xu ly tu dong cho 1 so truong
                if (row_user != null)
                {
                    mess = "Tài khoản đã tồn tại";
                    ViewBag.Error = "<span class='text-danger'>" + mess + "</span>";
                }
                else
                {
                    Users users = new Users();
                    users.Fullname = fullname;
                    users.Status = 1;
                    users.Role = "customer";
                    // xu ly cac truong nhap vao
                    users.Phone = phone;
                    users.Email = email;
                    users.Gender = "1";
                    users.UserName = username;
                    users.Password = password;
                    users.CreateAt = DateTime.Now;
                    usersDAO.Insert(users);
                    mess = "Tạo tài khoản thành công";
                    ViewBag.Error = "<span class='text-success'>" + mess + "</span>";
                }
            }
            return View();
        }
    }
}