using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Numerics;
using System.Security.Cryptography;
using Blockchain_MocChauMilk.Models;
using Blockchain_MocChauMilk.Common;

namespace Blockchain_MocChauMilk.Areas.Admin.Controllers
{
    public class KiemDinhsController : Controller
    {
        private MocChauModel db = new MocChauModel();

        // GET: Admin/KiemDinhs
        public ActionResult Index()
        {
            var quyTrinhs = db.QuyTrinhs.Include(q => q.LoHang).Include(q => q.NguoiDung).Include(q => q.SanPham);
            return View(quyTrinhs.ToList());
        }
        public ActionResult Quytrinhchuaxacthuc()
        {
            var quyTrinhs = db.QuyTrinhs.Include(q => q.LoHang).Include(q => q.NguoiDung).Include(q => q.SanPham).Where(x => x.TrangThai == 0);
            return View(quyTrinhs.ToList());
        }
        public ActionResult Quytrinhdaxacthuc()
        {
            var quyTrinhs = db.QuyTrinhs.Include(q => q.LoHang).Include(q => q.NguoiDung).Include(q => q.SanPham).Where(x => x.TrangThai==1);
            return View(quyTrinhs.ToList());
        }
        public ActionResult Quytrinhbisuadoi()
        {
            var quyTrinhs = db.QuyTrinhs.Include(q => q.LoHang).Include(q => q.NguoiDung).Include(q => q.SanPham).Where(x => x.TrangThai == 2);
            return View(quyTrinhs.ToList());
        }

        // GET: Admin/KiemDinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyTrinh quyTrinh = db.QuyTrinhs.Find(id);
            if (quyTrinh == null)
            {
                return HttpNotFound();
            }
            return View(quyTrinh);
        }

        // GET: Admin/KiemDinhs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang");
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: Admin/KiemDinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaQuyTrinh,TenQuyTrinh,MoTa,MaSanPham,ChuKi,MaNguoiDung,NgayKy,TrangThai,MaLoHang,TepTinChungThuc")] QuyTrinh quyTrinh)
        {
            if (ModelState.IsValid)
            {
                db.QuyTrinhs.Add(quyTrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang", quyTrinh.MaLoHang);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", quyTrinh.MaNguoiDung);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", quyTrinh.MaSanPham);
            return View(quyTrinh);
        }

        // GET: Admin/KiemDinhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyTrinh quyTrinh = db.QuyTrinhs.Find(id);
            if (quyTrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang", quyTrinh.MaLoHang);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", quyTrinh.MaNguoiDung);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", quyTrinh.MaSanPham);
            return View(quyTrinh);
        }

        // POST: Admin/KiemDinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaQuyTrinh,TenQuyTrinh,MoTa,MaSanPham,ChuKi,MaNguoiDung,NgayKy,TrangThai,MaLoHang,TepTinChungThuc")] QuyTrinh quyTrinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quyTrinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang", quyTrinh.MaLoHang);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "TaiKhoan", quyTrinh.MaNguoiDung);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", quyTrinh.MaSanPham);
            return View(quyTrinh);
        }

        // GET: Admin/KiemDinhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuyTrinh quyTrinh = db.QuyTrinhs.Find(id);
            if (quyTrinh == null)
            {
                return HttpNotFound();
            }
            return View(quyTrinh);
        }

        // POST: Admin/KiemDinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuyTrinh quyTrinh = db.QuyTrinhs.Find(id);
            db.QuyTrinhs.Remove(quyTrinh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Xacthuc()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KiemDinhQuyTrinh(FormCollection quytrinhKiemDinh)
        {

            String hash = "";
            SHA256 mySHA256 = SHA256.Create();
            String maQT = quytrinhKiemDinh["MaQuyTrinh"].ToString();
            String khoabimat = quytrinhKiemDinh["khoabimat"].ToString();
            if (khoabimat == null || khoabimat.Trim().Equals(""))
            {
                ViewBag.ThongBao = "Chưa nhập khoá";
                return View("Edit");
            }
            QuyTrinh qt = db.QuyTrinhs.Find(int.Parse(maQT));
            int maND = (int)qt.MaNguoiDung;
            NguoiDung ND = db.NguoiDungs.Find(maND);
            String soN = ND.SoN;
            BigInteger n = BigInteger.Parse(soN);
            BigInteger sk = BigInteger.Parse(khoabimat);
            string relativePath = qt.TepTinChungThuc.ToString();
            string pathFile = Server.MapPath("~" + relativePath);            
            FileStream fs= System.IO.File.OpenRead(pathFile);
            byte[] by = mySHA256.ComputeHash(fs);
            for (int i = 0; i < by.Length; i++)
                hash += MaHoaRSA.tranform_binary(by[i]);
            hash= MaHoaRSA.tranform_decimal(hash);
            // Chữ kí của cơ quan kiểm định
            string sig = MaHoaRSA.RSA_MaHoa(hash, sk, n).ToString();
            qt.ChuKi = sig;
            if (qt.ChuKi == null || qt.ChuKi.Trim().Equals(""))
            {
                ViewBag.ThongBao = "Ký lỗi chữ ký lỗi";
                return View("Edit");
            }
            else
            {
                qt.TrangThai = 1;
                db.SaveChanges();
                ViewBag.ThongBao = "Ký Thành công";
                return View("Edit");
            }
        }

    }
}
