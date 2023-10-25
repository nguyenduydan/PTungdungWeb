using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SupplierDAO
    {
        private MyDBContext db = new MyDBContext();

        public List<Suppliers> getList()
        {
            return db.Suppliers.ToList();
        }

        //INDEX dựa vào Status =1,2 còn Status= 0 == thùng rác
        public List<Suppliers> getList(string status = "All")
        {
            List<Suppliers> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Suppliers
                            .Where(m => m.Status != 0)
                            .ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Suppliers
                            .Where(m => m.Status == 0)
                            .ToList();
                        break;
                    }
                default:
                    {
                        list = db.Suppliers.ToList();
                        break;
                    }
            }

            return list;
        }

        //DETAILS
        public Suppliers getRow(int? id) //có ? hay không dựa vào details trong controller
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Suppliers.Find(id);
            }
        }

        //CREATE
        public int Insert(Suppliers row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }

        //UPDATE
        public int Update(Suppliers row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //DELETE
        public int Delete(Suppliers row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();
        }
    }
}
