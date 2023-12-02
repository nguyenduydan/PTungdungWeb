using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("ProductInfo")] //tên của bảng
    public class ProductInfo
    {
        public int Id { get; set; }
        [Display(Name = "Tên loại sản phẩm")]
        public int CatID { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Tên loại sản phẩm")]
        public string CatName { get; set; }

        //bo sung them truong Slug cua Categories: detail product
        public string CategorySlug { get; set; }
        [Display(Name = "Tên nhà cung cấp")]
        public int SupplierId { get; set; }
        [Display(Name = "Tên nhà cung cấp")]
        public string SupplierName { get; set; }

        public string Slug { get; set; }
        [Display(Name = "Ảnh")]
        public string Image { get; set; }
        [Display(Name = "Giá gốc")]
        public decimal Price { get; set; }
        [Display(Name = "Giá giảm")]
        public decimal SalePrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.###}", ApplyFormatInEditMode = true)]
        [Display(Name = "Số lượng")]
        public decimal Amount { get; set; }

        public string MetaDesc { get; set; }

        public string MetaKey { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateAt { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int Status { get; set; }

    }
}
