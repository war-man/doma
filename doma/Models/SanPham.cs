//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace doma.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        public SanPham()
        {
            this.ChiTietBoSanPhams = new HashSet<ChiTietBoSanPham>();
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
    
        public int ID { get; set; }
        public string Ten { get; set; }
        public int DioGia { get; set; }
        public string linkanh { get; set; }
        public string MoTa { get; set; }
        public string TinhTrang { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual ICollection<ChiTietBoSanPham> ChiTietBoSanPhams { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
