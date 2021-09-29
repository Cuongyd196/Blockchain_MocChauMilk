using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Blockchain_MocChauMilk.Common;
using Blockchain_MocChauMilk.Models;

namespace Blockchain_MocChauMilk.Controllers
{
    public class HomeController : Controller
    {
        MocChauModel db = new MocChauModel();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimKiem(string searchString)
        {
            var sp = from l in db.SanPhams
                     select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.TenSanPham.Contains(searchString));
            }

            return View(sp);
        }
        // GET: Sanpham
        public ActionResult SanphamPartialView()
        {

            var sp = db.SanPhams.ToList();
            return PartialView(sp);
        }

        public ActionResult XemChiTietSP(int? id)
        {
            int demQTDaXacThuc = 0;
            SHA256 mySHA256 = SHA256.Create();
            SanPham sanPham = db.SanPhams.Find(id);
            var quyTrinh = db.QuyTrinhs.Where(x => x.MaSanPham == id).ToList();
            // Duyệt từng quy trình sản xuất sản phẩm và kiểm tra chữ kí
            foreach (var qt in quyTrinh)
            {
                String hash = "";
                NguoiDung nd = db.NguoiDungs.Find(qt.MaNguoiDung);
                BigInteger e = BigInteger.Parse(nd.SoE);
                BigInteger n = BigInteger.Parse(nd.SoN);
                String ktchuki = MaHoaRSA.RSA_MaHoa(qt.ChuKi, e, n);
                String pathFile = "D:/DotNet/Blockchain_MocChauMilk/" + qt.TepTinChungThuc.ToString();
                FileStream fs = System.IO.File.OpenRead(pathFile);
                byte[] by = mySHA256.ComputeHash(fs);
                for (int i = 0; i < by.Length; i++)
                    hash += MaHoaRSA.tranform_binary(by[i]);
                hash =  MaHoaRSA.tranform_decimal(hash);
                // Kiểm tra chữ kí trên quy trình
                if(ktchuki.Equals(hash))
                {
                    qt.TrangThaiXacThuc = 1;
                    demQTDaXacThuc += 1;
                    db.SaveChanges();
                }
                else
                {
                    qt.TrangThaiXacThuc = 0;
                    qt.TrangThai = 2;
                    db.SaveChanges();
                }
            }
            // Nếu tất cả các quy trình sản xuất đều được xác thực là đúng
            if(demQTDaXacThuc == quyTrinh.Count && demQTDaXacThuc!=0)
            {
                sanPham.TrangThai = 1;
                db.SaveChanges();
            }
            else
            {
                sanPham.TrangThai = 0;
                db.SaveChanges();
            }
            ViewBag.TrangthaiSP = sanPham.TrangThai;
            ViewBag.TenSP = sanPham.TenSanPham;
            ViewBag.AnhSP = sanPham.HinhAnh;
            return View(quyTrinh);
        }

        public void XacThuc()
        {
           

        }
    }
}