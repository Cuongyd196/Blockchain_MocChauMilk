using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Blockchain_MocChauMilk.Common;
using Blockchain_MocChauMilk.Models;

namespace Blockchain_MocChauMilk.Areas.Admin.Controllers
{
    public class QuyTrinhsController : Controller
    {
        private MocChauModel db = new MocChauModel();

        // GET: Admin/QuyTrinhs
        public ActionResult Index(string searchString)
        {
            var quyTrinhs = db.QuyTrinhs.Include(q => q.LoHang).Include(q => q.NguoiDung).Include(q => q.SanPham);
            if (!String.IsNullOrEmpty(searchString))
            {
                quyTrinhs = quyTrinhs.Where(s => s.SanPham.TenSanPham.Contains(searchString));
            }
            return View(quyTrinhs.ToList());
        }

        // GET: Admin/QuyTrinhs/Details/5
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

        // GET: Admin/QuyTrinhs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang");
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "MoTa");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: Admin/QuyTrinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Create([Bind(Include = "MaQuyTrinh,TenQuyTrinh,MoTa,MaSanPham,ChuKi,MaNguoiDung,NgayKy,TrangThai,MaLoHang,TepTinChungThuc")] QuyTrinh quyTrinh)
        {
            if (ModelState.IsValid)
            {
                quyTrinh.TrangThai = 0;
                db.QuyTrinhs.Add(quyTrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang", quyTrinh.MaLoHang);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "MoTa", quyTrinh.MaNguoiDung);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", quyTrinh.MaSanPham);
            return View(quyTrinh);
        }

        // GET: Admin/QuyTrinhs/Edit/5
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
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "MoTa", quyTrinh.MaNguoiDung);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", quyTrinh.MaSanPham);
            return View(quyTrinh);
        }

        // POST: Admin/QuyTrinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        //public ActionResult Edit([Bind(Include = "MaQuyTrinh,TenQuyTrinh,MoTa,MaSanPham,ChuKi,MaNguoiDung,NgayKy,TrangThai,MaLoHang,TepTinChungThuc")] QuyTrinh quyTrinh)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //QuyTrinh qt = db.QuyTrinhs.Find(quyTrinh.MaQuyTrinh);
        //        //quyTrinh.TrangThai = qt.TrangThai;
        //        db.Entry(quyTrinh).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang", quyTrinh.MaLoHang);
        //    ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "MoTa", quyTrinh.MaNguoiDung);
        //    ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", quyTrinh.MaSanPham);
        //    return View(quyTrinh);
        //}

        public ActionResult Edit(QuyTrinh quyTrinh)
        {
            if (quyTrinh !=null)
            {
                QuyTrinh qt = db.QuyTrinhs.Find(quyTrinh.MaQuyTrinh);

                qt.TrangThai = qt.TrangThai;
                qt.MaLoHang = quyTrinh.MaLoHang;
                qt.MaNguoiDung = quyTrinh.MaNguoiDung;
                qt.MaSanPham = quyTrinh.MaSanPham;
                qt.TenQuyTrinh = quyTrinh.TenQuyTrinh;
                qt.TepTinChungThuc = quyTrinh.TepTinChungThuc;
                qt.NgayKy = qt.NgayKy;
                qt.MoTa = quyTrinh.MoTa;
                qt.ChuKi = qt.ChuKi;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoHang = new SelectList(db.LoHangs, "MaLoHang", "TenLoHang", quyTrinh.MaLoHang);
            ViewBag.MaNguoiDung = new SelectList(db.NguoiDungs, "MaNguoiDung", "MoTa", quyTrinh.MaNguoiDung);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", quyTrinh.MaSanPham);
            return View(quyTrinh);
        }
            // GET: Admin/QuyTrinhs/Delete/5
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

        // POST: Admin/QuyTrinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
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

        //public void XacThuc()
        //{
        //    String hash = "";
        //    SHA256 mySHA256 = SHA256.Create();
        //    // lấy ra tất cả quy trình
        //    var quytrinh = db.QuyTrinhs.ToList();
        //    // lấy ra tất cả sản phẩm
        //    var sanpham = db.SanPhams.ToList();
            
        //    int[] arrSP = new int[100];
        //    int j = 0;
        //    foreach (var i in sanpham)
        //    {
        //        arrSP[j] = i.MaSanPham;
        //        j++;
        //    }
        //    foreach (var qt in  quytrinh)
        //    {
        //        if(qt.MaSanPham == arrSP[j])
        //        {

        //            NguoiDung nd = db.NguoiDungs.Find(qt.MaNguoiDung);
        //            BigInteger e = BigInteger.Parse(nd.SoE);
        //            BigInteger n = BigInteger.Parse(nd.SoN);
        //            BigInteger q = BigInteger.Parse(MaHoaRSA.RSA_encription(qt.ChuKi, e, n));
        //            String pathFile = "D:/DotNet/Blockchain_MocChauMilk/" + qt.TepTinChungThuc.ToString();
        //            FileStream fs = System.IO.File.OpenRead(pathFile);
        //            byte[] by = mySHA256.ComputeHash(fs);
        //            //for (int i = 0; i < by.Length; i++)
        //            //    hash += MaHoaRSA.tranform_binary(by[i]);
        //            //BigInteger hash =  MaHoaRSA.tranform_decimal(hash);
        //        }
        //        j++;
        //    }

        //}
    }
}
