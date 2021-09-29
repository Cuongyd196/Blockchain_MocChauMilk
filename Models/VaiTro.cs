namespace Blockchain_MocChauMilk.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VaiTro")]
    public partial class VaiTro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VaiTro()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        [Key]
        public int MaVaitro { get; set; }

        [Column("VaiTro")]
        [StringLength(100)]
        [DisplayName("Tên quyền")]
        public string VaiTro1 { get; set; }

        [StringLength(500)]
        [DisplayName("Thông tin khác")]
        public string ThongTinKhoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
