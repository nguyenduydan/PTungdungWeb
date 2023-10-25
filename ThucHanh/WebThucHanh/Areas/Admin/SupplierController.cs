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

namespace WebThucHanh.Areas.Admin
{
    public class SupplierController : Controller
    {
        SupplierDAO supplierDAO = new SupplierDAO();

        //-----------------------------------------------------------------------------
        // GET: Admin/Category/Index
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //Hiển thị thông báo
                TempData["message"] = new Xmessage("danger", "Không tìm thấy nhà cung cấp!");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = supplierDAO.getRow(id);
            if (suppliers == null)
            {
                //hiển thị thông báo
                TempData["message"] = new Xmessage("danger", "Không tìm thấy nhà cung cấp!");
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.OrderList = new SelectList(supplierDAO.getList("Index"), "ID", "Name");//cái nào hiển thị ở trang index thì hiển thị để lựa
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                //Xử lý tự động 1 số trường
                //Xử lý tự động cho các trường sau:
                //---Create At
                suppliers.CreateAt = DateTime.Now;
                //---Create By
                suppliers.CreateBy = Convert.ToInt32(Session["UserId"]);
                //Slug
                suppliers.Slug = Xstring.Str_Slug(suppliers.Name);
                //Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order += 1;
                }
                //Update at
                suppliers.UpdateAt = DateTime.Now;
                //Update by
                suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);
                //Thêm mới vào database
                supplierDAO.Insert(suppliers);
                //hiển thị thông báo thành công
                TempData["message"] = new Xmessage("success", "Thêm mới nhà cung cấp thành công!");
                return RedirectToAction("Index"); ;
            }
            ViewBag.OrderList = new SelectList(supplierDAO.getList("Index"), "ID", "Name");
            return View(suppliers);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.OrderList = new SelectList(supplierDAO.getList("Index"), "ID", "Name");
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Không tìm thấy nhà cung cấp!");
                return RedirectToAction("Index");
            }
            Suppliers suppliers =supplierDAO.getRow(id);
            if (suppliers == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Không tìm thấy nhà cung cấp!");
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                //Xử lý tự động 1 số trường
                //Xử lý tự động cho các trường sau:
                //Slug
                suppliers.Slug = Xstring.Str_Slug(suppliers.Name);
                //Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order += 1;
                }
                //Update at
                suppliers.UpdateAt = DateTime.Now;
                //Update by
                suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);
                supplierDAO.Update(suppliers);
                //hiển thị thông báo thành công
                TempData["message"] = new Xmessage("success", "Cập nhật thông tin thành công!");
                return RedirectToAction("Index"); ;
            }
            ViewBag.OrderList = new SelectList(supplierDAO.getList("Index"), "ID", "Name");
            return View(suppliers);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = supplierDAO.getRow(id);
            if (suppliers == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Trash");
            }
            return View(suppliers);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suppliers suppliers = supplierDAO.getRow(id);
            supplierDAO.Delete(suppliers);
            return RedirectToAction("Trash");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Cập nhật trạng thái thất bại!");
                return RedirectToAction("Index");
            }

            Suppliers suppliers = supplierDAO.getRow(id);
            if (suppliers == null)
            {
                TempData["message"] = new Xmessage("danger", "Cập nhật trạng thái thất bại!");
                return RedirectToAction("Index");
            }
            //Cập nhật trạng thái
            suppliers.Status = (suppliers.Status == 1) ? 2 : 1;
            //cập nhật update at
            suppliers.UpdateAt = DateTime.Now;
            //cập nhật update by
            suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);
            //xác nhận DB (update DB)
            supplierDAO.Update(suppliers);
            //hiện thị thông báo
            TempData["message"] = new Xmessage("success", "Cập nhật trạng thái thành công!");
            //trở về trang index
            return RedirectToAction("Index");
        }

        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Index");
            }

            Suppliers suppliers = supplierDAO.getRow(id);
            if (suppliers == null)
            {
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Index");
            }
            //Cập nhật trạng thái
            suppliers.Status = 0;
            //cập nhật update at
            suppliers.UpdateAt = DateTime.Now;
            //cập nhật update by
            suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);
            //xác nhận DB (update DB)
            supplierDAO.Update(suppliers);
            //hiện thị thông báo
            TempData["message"] = new Xmessage("success", "Xóa mẫu tin thành công!");
            //trở về trang index
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            return View(supplierDAO.getList("Trash"));
        }
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Phục hồi mẫu tin thất bại!");
                return RedirectToAction("Index");
            }

            Suppliers suppliers = supplierDAO.getRow(id);
            if (suppliers == null)
            {
                TempData["message"] = new Xmessage("danger", "Phục hồi mẫu tin thất bại!");
                return RedirectToAction("Index");
            }
            //Cập nhật trạng thái status = 2
            suppliers.Status = 2;
            //cập nhật update at
            suppliers.UpdateAt = DateTime.Now;
            //cập nhật update by
            suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);
            //xác nhận DB (update DB)
            supplierDAO.Update(suppliers);
            //hiện thị thông báo
            TempData["message"] = new Xmessage("success", "Phục hồi mẫu tin thành công!");
            //ở lại tiếp tục lục thùng rác
            return RedirectToAction("Trash");
        }
    }
}
