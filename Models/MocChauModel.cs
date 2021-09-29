namespace Blockchain_MocChauMilk.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MocChauModel : DbContext
    {
        public MocChauModel()
            : base("name=MocChauModel")
        {
        }

        public virtual DbSet<LoHang> LoHangs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<QuyTrinh> QuyTrinhs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<VaiTro> VaiTroes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TaiKhoan)
                .IsFixedLength();

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.HinhAnh)
                .IsFixedLength();
        }
    }
}
