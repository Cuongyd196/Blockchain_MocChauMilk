namespace Blockchain_MocChauMilk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            QuyTrinhs = new HashSet<QuyTrinh>();
        }

        [Key]
        public int MaNguoiDung { get; set; }

        [StringLength(30)]
        [DisplayName("Tài khoản")]

        public string TaiKhoan { get; set; }

        [StringLength(20)]
        [DisplayName("Mật khẩu")]

        public string MatKhau { get; set; }

        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [DisplayName("Quyền")]
        public int? MaVaiTro { get; set; }

        [StringLength(500)]
        [DisplayName("Số E")]
        public string SoE { get; set; }

        [StringLength(500)]
        [DisplayName("Số N")]

        public string SoN { get; set; }

        public virtual VaiTro VaiTro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuyTrinh> QuyTrinhs { get; set; }
    }
}
