using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using Blockchain_MocChauMilk.Models;
using Blockchain_MocChauMilk.Common;

namespace Blockchain_MocChauMilk.Areas.Admin.Controllers
{
    public class TaoMoiRSAController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaoKhoa()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaoKhoa(FormCollection Thongtin)
        {
            MocChauModel db = new MocChauModel();
            // Lấy ra Mã cơ quan kiểm định từ View
            string MaND = Thongtin["MaNguoiDung"].ToString();
            // Gọi đến hàm khởi tạo và sinh khóa RSA tự động
            KhoiTao();
            TaoMoiKhoa();
            // Tìm ra cơ quan kiểm định đang tạo khóa trong CSDL
            NguoiDung nguoidung = db.NguoiDungs.Find(int.Parse(MaND));
            // Lưu lại khóa công khai của CQ Kiểm định vào CSDL
            nguoidung.SoE = "" +e;
            nguoidung.SoN = "" + N;
            db.SaveChanges();
            if (d != 0)
            {
                // Hiển thị khóa bí mật của CQ Kiểm định
                ViewBag.thongbao = "Khóa bí mật của bạn là :";
                ViewBag.d = "" +d;
                ViewBag.canthan = "Lưu ý không nên để lộ khóa bí mật cho bất kì ai";
            }
            else
            {
                ViewBag.loi = " Tạo khóa lỗi";
            }
            return View();
        }
        class RandomBigInteger : Random
        {
            public RandomBigInteger() : base()
            {
            }

            public RandomBigInteger(int Seed) : base(Seed)
            {
            }

            public BigInteger NextBigInteger(int bitLength)
            {
                if (bitLength < 1) return BigInteger.Zero;
                int bytes = bitLength / 8;
                int bits = bitLength % 8;
                byte[] bs = new byte[bytes + 1];
                NextBytes(bs);
                byte mask = (byte)(0xFF >> (8 - bits));
                bs[bs.Length - 1] &= mask;
                return new BigInteger(bs);
            }
        }
        BigInteger PK = 0, SK = 0, Modulo = 0;

        BigInteger p = 0, q = 0, N = 0, n = 0, e = 0, d = 0, mid_e = 0;
        int length = 256;
        RandomBigInteger random_big = new RandomBigInteger();
        Random random = new Random();
        // GET: Admin/TaoMoiRSA
        void KhoiTao()
        {
            do
                p = random_odd(length);
            while (is_prime(p) == false);
            while (true)
            {
                q = random_odd(length);
                if (is_prime(q) && p != q) break;
            }
            N = p * q;
            n = (p - 1) * (q - 1);
        }
        void TaoMoiKhoa()
        {
            while (true)
            {
                e = random_big.NextBigInteger(random.Next(2, get_bit(N) - 1));
                e += 2;
                if (ucln(e, n) == 1 && e < N) break;
            };
            d = inverse_number(e, n);
            mid_e = inverse_number(d, n);
        }

        bool is_prime(BigInteger p)
        {
            if (p == 3 || p == 5 || p == 7 || p == 11 || p == 13 || p == 17) return true;
            BigInteger a, k = 0, q = 0;

            bool kt = false;
            transform(p, ref k, ref q);
            for (int i = 0; i < 10; i++)
            {
                a = i + 3;
                if (miller_rabin(p, k, q, a) == true) kt = true;
                if (kt == false) return false;
            }
            return true;
        }

        BigInteger random_odd(int length)
        {
            BigInteger a = 10;

            do
                a = random_big.NextBigInteger(length);
            while (a % 2 == 0);
            return a;
        }

        int get_bit(BigInteger n)
        {
            int dem = 0;

            while (n > 0)
            {
                n = n / 2;
                dem++;
            }
            return dem;
        }
        public string tranform_binary(BigInteger n)
        {
            int i;
            string st = "";
            BigInteger[] a = new BigInteger[3000];

            for (i = 0; n > 0; i++)
            {
                a[i] = n % 2;
                n = n / 2;
            }

            for (i = i - 1; i >= 0; i--)
                st += a[i];
            return st;
        }

        public string tranform_decimal(string st)
        {
            BigInteger n = BigInteger.Parse(st);
            BigInteger kq = 0;
            BigInteger luy_thua = 1;
            while (n != 0)
            {
                BigInteger r = n % 10;
                n = n / 10;
                if (r == 1)
                    kq += luy_thua;
                luy_thua = 2 * luy_thua;
            }
            return "" + kq;
        }

        void transform(BigInteger p, ref BigInteger k, ref BigInteger q)
        {
            p--;
            while (p % 2 == 0)
            {
                k++;
                p = p / 2;
            }
            q = p;
        }

        bool miller_rabin(BigInteger p, BigInteger k, BigInteger q, BigInteger a)
        {

            BigInteger x = fast_exp(a, q, p);

            if (x == 1) return true;
            for (int i = 0; i < k; i++)
            {
                if (x == p - 1) return true;
                x = x * x % p;
            }
            return false;
        }

        BigInteger fast_exp(BigInteger m, BigInteger e, BigInteger p)
        {
            BigInteger kq = 1;
            int i, n = 0;
            BigInteger[] b = new BigInteger[809060];

            for (i = 1; e > 0; i++)
            {
                b[i] = e % 2;
                e = e / 2;
            }
            n = i;

            for (i = n; i >= 1; i--)
            {
                kq = kq * kq % p;
                if (b[i] == 1)
                    kq = kq * m % p;
            }
            return kq;
        }

        BigInteger inverse_number(BigInteger a, BigInteger m)
        {
            BigInteger y = 0, y0 = 0, y1 = 1, r = 1, q = 1, m1 = m;

            while (a > 0)
            {
                r = m % a;
                if (r == 0) break;
                q = m / a;
                y = y0 - y1 * q;
                y0 = y1;
                y1 = y;
                m = a;
                a = r;
            }
            if (a > 1)
                return -1;
            else
            {
                if (y < 0) y = m1 + y;
                return y;
            }
        }
        BigInteger ucln(BigInteger a, BigInteger b)
        {
            BigInteger temp;

            while (b != 0)
            {
                temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }

    }
}