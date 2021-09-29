namespace Blockchain_MocChauMilk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            QuyTrinhs = new HashSet<QuyTrinh>();
        }

        [Key]
        public int MaSanPham { get; set; }

        [StringLength(50)]
        [DisplayName("Tên sản phẩm")]
        public string TenSanPham { get; set; }

        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [StringLength(50)]
        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }

        [DisplayName("Lô hàng")]
        public int? MaLoHang { get; set; }

        [DisplayName("Trạng thái")]
        public int? TrangThai { get; set; }

        public virtual LoHang LoHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuyTrinh> QuyTrinhs { get; set; }
    }
}
