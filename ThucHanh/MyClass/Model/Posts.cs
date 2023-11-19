using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Posts")] //tên của bảng
    public class Posts
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="Chủ đề bài viết")]
        public int? TopID { get; set; }
        [Required(ErrorMessage ="Không được để trống")]
        [Display(Name = "Tên bài viết")]
        public string Title { get; set; }
        [Display(Name = "Liên kết")]
        public string Slug { get; set; }
        [Display(Name = "Chi tiết")]
        public string Detail { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Kiểu bài viết")]
        public string PostType { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Mô tả")]
        public string MetaDesc { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Từ khóa")]
        public string MetaKey { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Người tạo")]
        public int CreateBy { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Ngày tạo")]
        public DateTime CreateAt { get; set; }
        [Display(Name = "Người cập nhật  ")]
        public int? UpdateBy { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdateAt { get; set; }
        [Required]
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }

    }
}
