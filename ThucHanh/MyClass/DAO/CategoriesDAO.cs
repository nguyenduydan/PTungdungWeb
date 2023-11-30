using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyClass.Model;

namespace MyClass.DAO
{

    public class CategoriesDAO
    {
        private MyDBContext db = new MyDBContext();

        public List<Categories> getList()
        {
            return db.Categories.ToList();
        }

        //INDEX dựa vào Status =1,2 còn Status= 0 == thùng rác
        public List<Categories> getList(string status = "All")
        {
            List<Categories> list = null;
            switch(status)
            {
                case "Index":
                    {
                        list = db.Categories
                            .Where(m => m.Status != 0)
                            .ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Categories
                            .Where(m => m.Status == 0)
                            .ToList();
                        break;
                    }
                default:
                    {
                        list = db.Categories.ToList();
                        break;
                    }
            }
            
            return list;
        }
        public List<Categories> getListByPareantId(int parentid = 0)
        {
            return db.Categories
              .Where(m => m.ParentID == parentid && m.Status == 1)
              .OrderBy(m => m.Order)
              .ToList();
        }

        //DETAILS
        public Categories getRow(int? id) //có ? hay không dựa vào details trong controller
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Categories.Find(id);
            }
        }

        //Hien thi danh sach 1 mau tin (ban ghi) voi kieu string = slug
        public Categories getRow(string slug)
        {
            return db.Categories
              .Where(m => m.Slug == slug && m.Status == 1)
              .FirstOrDefault();
        }


        //CREATE
        public int Insert(Categories row)
        {
            db.Categories.Add(row);
            return db.SaveChanges();
        }

        //UPDATE
        public int Update(Categories row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //DELETE
        public int Delete(Categories row)
        {
            db.Categories.Remove(row);
            return db.SaveChanges();
        }
    }
}