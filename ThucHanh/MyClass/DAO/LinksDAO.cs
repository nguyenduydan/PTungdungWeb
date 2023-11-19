using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class LinksDAO
    {
        private MyDBContext db = new MyDBContext();
        
        //Hiển thị danh sách toàn bộ loại sản phẩm : select * from
        public List<Links> getList(string status = "All")
        {
            List<Links> list = null;
            return list;
        }

        //Hiên thị danh sách 1 mẫu tin (bản ghi)
        public Links getRow(int tableid, string typelink)
        {
            return db.Links
                .Where(m => m.TableID ==  tableid && m.Type ==  typelink)
                .FirstOrDefault(); //lấy kí tự đầu tiên
        }
        public Links getRow(string slug)
        {
            return db.Links
                .Where(m => m.Slug == slug)
                .FirstOrDefault();
        }

        //thêm mới mẫu tin
        public int Insert(Links row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }

        //Cập nhật một mẫu tin
        public int Update(Links row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //Xóa một mẫu tin
        public int Delete(Links row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}
