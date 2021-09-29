namespace Blockchain_MocChauMilk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuyTrinh")]
    public partial class QuyTrinh
    {
        [Key]
        public int MaQuyTrinh { get; set; }

        [StringLength(50)]
        [DisplayName("Tên quy trình")]
        public string TenQuyTrinh { get; set; }
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
        [DisplayName("Sản phẩm")]
        public int? MaSanPham { get; set; }
        [DisplayName("Chữ ký")]
        public string ChuKi { get; set; }
        [DisplayName("Cơ quan kiểm định")]
        public int? MaNguoiDung { get; set; }
        [DisplayName("Ngày ký")]
        public DateTime? NgayKy { get; set; }
        [DisplayName("Trạng thái")]
        public int? TrangThai { get; set; }
        [DisplayName("Lô hàng")]
        public int? MaLoHang { get; set; }
        [DisplayName("Tệp tin chứng thực")]
        public string TepTinChungThuc { get; set; }

        [DisplayName("Trạng thái xác thực")]
        public int? TrangThaiXacThuc { get; set; }

        public virtual LoHang LoHang { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
