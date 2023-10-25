using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
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
                //Xử lý thông tin cho hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Image = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/supplier/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);   
                    }
                }//ket thuc phan upload hinh anh
                //Thêm mới vào database
                supplierDAO.Insert(suppliers);
                //hiển thị thông báo thành công
                TempData["message"] = new Xmessage("success", "Thêm mới nhà cung cấp thành công!");
                return RedirectToAction("Index"); ;
            }
            ViewBag.OrderList = new SelectList(supplierDAO.getList("Index"), "ID", "Name");
            return View(suppliers);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.OrderList = new SelectList(supplierDAO.getList("Index"), "Order", "Name");
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Không tìm thấy nhà cung cấp!");
                return RedirectToAction("Index");
            }
            Suppliers suppliers = supplierDAO.getRow(id);
            if (suppliers == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Không tìm thấy nhà cung cấp!");
                return RedirectToAction("Index"); ;
            }
            return View(suppliers);
        }

        // POST: Admin/Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                //Xu ly cho muc Slug
                suppliers.Slug = Xstring.Str_Slug(suppliers.Name);
                //chuyen doi dua vao truong Name de loai bo dau, khoang cach = dau -

                //Xu ly cho muc Order
                if (suppliers.Order == null)
                {
                    suppliers.Order = 1;
                }
                else
                {
                    suppliers.Order = suppliers.Order + 1;
                }

                //xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = suppliers.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        suppliers.Image = imgName;
                        //upload hinh
                        string PathDir = "~/Public/img/supplier/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);

                        //cap nhat thi phai xoa file cu
                        //Xoa file
                        if (suppliers.Image != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Image);
                            System.IO.File.Delete(DelPath);
                        }

                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh

                //Xu ly cho muc UpdateAt
                suppliers.UpdateAt = DateTime.Now;

                //Xu ly cho muc UpdateBy
                suppliers.UpdateBy = Convert.ToInt32(Session["UserId"]);

                supplierDAO.Update(suppliers);

                //Thong bao thanh cong
                TempData["message"] = new Xmessage("success", "Sửa danh mục thành công");
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }


        // GET: Admin/Supplier/Delete/5
        //Xóa hoàn toàn
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //hiện thị thông báo
                TempData["message"] = new Xmessage("danger", "Xóa mẫu tin thất bại!");
                return RedirectToAction("Trash");
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
            if (supplierDAO.Delete(suppliers) == 1)
            {
                //duong dan den anh can xoa
                string PathDir = "~/Public/img/supplier/";
                //cap nhat thi phai xoa file cu
                if (suppliers.Image != null)
                {
                    string DelPath = Path.Combine(Server.MapPath(PathDir), suppliers.Image);
                    System.IO.File.Delete(DelPath);
                }
            }
            //Thong bao thanh cong
            TempData["message"] = new Xmessage("success", "Xóa danh mục thành công");
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

        //Xóa bỏ vào thùng rác
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
