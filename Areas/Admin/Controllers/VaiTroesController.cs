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
    public class VaiTroesController : Controller
    {
        private MocChauModel db = new MocChauModel();

        // GET: Admin/VaiTroes
        public ActionResult Index()
        {
            return View(db.VaiTroes.ToList());
        }

        // GET: Admin/VaiTroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTro vaiTro = db.VaiTroes.Find(id);
            if (vaiTro == null)
            {
                return HttpNotFound();
            }
            return View(vaiTro);
        }

        // GET: Admin/VaiTroes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VaiTroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVaitro,VaiTro1,ThongTinKhoa")] VaiTro vaiTro)
        {
            if (ModelState.IsValid)
            {
                db.VaiTroes.Add(vaiTro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaiTro);
        }

        // GET: Admin/VaiTroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTro vaiTro = db.VaiTroes.Find(id);
            if (vaiTro == null)
            {
                return HttpNotFound();
            }
            return View(vaiTro);
        }

        // POST: Admin/VaiTroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVaitro,VaiTro1,ThongTinKhoa")] VaiTro vaiTro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaiTro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaiTro);
        }

        // GET: Admin/VaiTroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTro vaiTro = db.VaiTroes.Find(id);
            if (vaiTro == null)
            {
                return HttpNotFound();
            }
            return View(vaiTro);
        }

        // POST: Admin/VaiTroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VaiTro vaiTro = db.VaiTroes.Find(id);
            db.VaiTroes.Remove(vaiTro);
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
