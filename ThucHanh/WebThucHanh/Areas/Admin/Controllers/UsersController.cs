using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using WebThucHanh.Library;

namespace WebThucHanh.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        UsersDAO usersDAO = new UsersDAO();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(usersDAO.getList("Index"));
        }
        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Trash");
            }
            Users Users = usersDAO.getRow(id);
            if (Users == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Trash");
            }
            return View(Users);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users Users = usersDAO.getRow(id);
            usersDAO.Delete(Users);
            //hiện thị thông báo
            TempData["message"] = new Xmessage("success", "Xóa mẫu tin thành công!");
            return RedirectToAction("Trash");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Index");
            }

            Users Users = usersDAO.getRow(id);
            if (Users == null)
            {
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Index");
            }
            //Cập nhật trạng thái
            Users.Status = 0;
            //cập nhật update at
            Users.UpdateAt = DateTime.Now;
            //cập nhật update by
            Users.UpdateBy = Convert.ToInt32(Session["UserId"]);
            //xác nhận DB (update DB)
            usersDAO.Update(Users);
            //hiện thị thông báo
            TempData["message"] = new Xmessage("success", "Xóa mẫu tin thành công!");
            //trở về trang index
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            return View(usersDAO.getList("Trash"));
        }
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Phục hồi mẫu tin thất bại!");
                return RedirectToAction("Index");
            }

            Users Users = usersDAO.getRow(id);
            if (Users == null)
            {
                TempData["message"] = new Xmessage("danger", "Phục hồi mẫu tin thất bại!");
                return RedirectToAction("Index");
            }
            //Cập nhật trạng thái status = 1
            Users.Status = 1;
            //cập nhật update at
            Users.UpdateAt = DateTime.Now;
            //cập nhật update by
            Users.UpdateBy = Convert.ToInt32(Session["UserId"]);
            //xác nhận DB (update DB)
            usersDAO.Update(Users);
            //hiện thị thông báo
            TempData["message"] = new Xmessage("success", "Phục hồi mẫu tin thành công!");
            //ở lại tiếp tục lục thùng rác
            return RedirectToAction("Trash");
        }
    }
}
