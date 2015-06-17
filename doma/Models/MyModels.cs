using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace doma.Models
{
    public class ThongTinNguoiDungMuaHang
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "Địa chỉ giao hàng")]
        public string DiaChiGiao { get; set; }

        [Required]
        [Display(Name = "Thời gian giao hàng")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime ThoiGianGiao { get; set; }

        [Required]
        [Display(Name = "Điện thoại liên lạc")]
        public string SoDienThoai { get; set; }
    }
}