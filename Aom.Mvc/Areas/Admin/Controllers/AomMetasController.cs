using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomLogic;

namespace Aom.Mvc.Areas.Admin.Controllers
{
    public class AomMetasController : Controller
    {
        private AomDbContext db = new AomDbContext();

        // GET: Admin/AomMetas
        public ActionResult Index()
        {
            return View(db.AomMetas.ToList());
        }

        // GET: Admin/AomMetas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomMeta aomMeta = db.AomMetas.Find(id);
            if (aomMeta == null)
            {
                return HttpNotFound();
            }
            return View(aomMeta);
        }

        // GET: Admin/AomMetas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AomMetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Display")] AomMeta aomMeta)
        {
            if (ModelState.IsValid)
            {
                aomMeta.Id = Guid.NewGuid();
                db.AomMetas.Add(aomMeta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aomMeta);
        }

        // GET: Admin/AomMetas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomMeta aomMeta = db.AomMetas.Find(id);
            if (aomMeta == null)
            {
                return HttpNotFound();
            }
            return View(aomMeta);
        }

        // POST: Admin/AomMetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Display")] AomMeta aomMeta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aomMeta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aomMeta);
        }

        // GET: Admin/AomMetas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomMeta aomMeta = db.AomMetas.Find(id);
            if (aomMeta == null)
            {
                return HttpNotFound();
            }
            return View(aomMeta);
        }

        // POST: Admin/AomMetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AomMeta aomMeta = db.AomMetas.Find(id);
            db.AomMetas.Remove(aomMeta);
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
