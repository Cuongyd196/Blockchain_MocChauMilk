using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blockchain_MocChauMilk.Models;

namespace Blockchain_MocChauMilk.Areas.Admin.Controllers
{
    public class LoHangsController : Controller
    {
        private MocChauModel db = new MocChauModel();

        // GET: Admin/LoHangs
        public ActionResult Index()
        {
            return View(db.LoHangs.ToList());
        }

        // GET: Admin/LoHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoHang loHang = db.LoHangs.Find(id);
            if (loHang == null)
            {
                return HttpNotFound();
            }
            return View(loHang);
        }

        // GET: Admin/LoHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoHang,TenLoHang,NgaySanXuat,GhiChu")] LoHang loHang)
        {
            if (ModelState.IsValid)
            {
                db.LoHangs.Add(loHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loHang);
        }

        // GET: Admin/LoHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoHang loHang = db.LoHangs.Find(id);
            if (loHang == null)
            {
                return HttpNotFound();
            }
            return View(loHang);
        }

        // POST: Admin/LoHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoHang,TenLoHang,NgaySanXuat,GhiChu")] LoHang loHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loHang);
        }

        // GET: Admin/LoHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoHang loHang = db.LoHangs.Find(id);
            if (loHang == null)
            {
                return HttpNotFound();
            }
            return View(loHang);
        }

        // POST: Admin/LoHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoHang loHang = db.LoHangs.Find(id);
            db.LoHangs.Remove(loHang);
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
    }
}
