using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Categories")] //tên của bảng
    public class Categories
    {
        [Key] 
        public int ID { get; set;}

        [Required (ErrorMessage = " Tên loại hàng không được để trống")]
        [Display (Name= "Tên Loại Hàng")]
        public string Name { get; set;}
        [Display(Name = "Tên Rút Gọn")]
        [Required(ErrorMessage = "Tên rút gọn không được để trống")]
        public string Slug { get; set;}
        [Display(Name = "Cấp cha")]
        [Required(ErrorMessage = "Cấp cha không được để trống")]
        public int? ParentID{ get; set;}
        [Display(Name = "Sắp xếp")]
        [Required(ErrorMessage = "Sắp xếp không được để trống")]
        public int? Order{ get; set;}
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "Mô tả")]
        public string MetaDesc { get; set;}
        [Required(ErrorMessage = "Từ khóa không được để trống")]
        [Display(Name = "Từ khóa")]
        public string MetaKey { get; set;}
        [Display(Name = "Người tạo")]
        [Required(ErrorMessage = "Người tạo không được để trống")]
        public int CreateBy { get; set;}
        [Display(Name = "Ngày tạo")]
        [Required(ErrorMessage = "Ngày tạo không được để trống")]
        public DateTime CreateAt { get; set;}
        [Required(ErrorMessage = "Người cập nhật không được để trống")]
        [Display(Name = "Người cập nhật")]
        public int UpdateBy { get; set;}
        [Required(ErrorMessage = "Người tạo không được để trống")]
        [Display(Name = "Người tạo")]
        public DateTime UpdateAt { get; set; }
        [Required]
        public int Status { get; set;}

    }
}
