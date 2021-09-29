namespace Blockchain_MocChauMilk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoHang")]
    public partial class LoHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoHang()
        {
            QuyTrinhs = new HashSet<QuyTrinh>();
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [DisplayName("Mã lô hàng")]
        public int MaLoHang { get; set; }

        [StringLength(50)]
        [DisplayName("Tên lô hàng")]
        public string TenLoHang { get; set; }

        [DisplayName("Ngày sản xuất")]
        [DataType(DataType.Date)]
        public DateTime? NgaySanXuat { get; set; }

        [StringLength(500)]
        [DisplayName("Ghi chú")]

        public string GhiChu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuyTrinh> QuyTrinhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
