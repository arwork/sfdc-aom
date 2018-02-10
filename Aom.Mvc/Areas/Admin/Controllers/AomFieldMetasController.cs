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
    public class AomFieldMetasController : Controller
    {
        private AomDbContext db = new AomDbContext();

        // GET: Admin/AomFieldMetas
        public ActionResult Index()
        {
            var aomFieldMetas = db.AomFieldMetas.Include(a => a.AomMeta).Include(a => a.FieldType);
            return View(aomFieldMetas.ToList());
        }

        // GET: Admin/AomFieldMetas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomFieldMeta aomFieldMeta = db.AomFieldMetas.Find(id);
            if (aomFieldMeta == null)
            {
                return HttpNotFound();
            }
            return View(aomFieldMeta);
        }

        // GET: Admin/AomFieldMetas/Create
        public ActionResult Create()
        {
            ViewBag.AomMetaId = new SelectList(db.AomMetas, "Id", "Name");
            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name");
            return View();
        }

        // POST: Admin/AomFieldMetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AomMetaId,Name,FieldTypeId,Display")] AomFieldMeta aomFieldMeta)
        {
            if (ModelState.IsValid)
            {
                aomFieldMeta.Id = Guid.NewGuid();
                db.AomFieldMetas.Add(aomFieldMeta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AomMetaId = new SelectList(db.AomMetas, "Id", "Name", aomFieldMeta.AomMetaId);
            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name", aomFieldMeta.FieldTypeId);
            return View(aomFieldMeta);
        }

        // GET: Admin/AomFieldMetas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomFieldMeta aomFieldMeta = db.AomFieldMetas.Find(id);
            if (aomFieldMeta == null)
            {
                return HttpNotFound();
            }
            ViewBag.AomMetaId = new SelectList(db.AomMetas, "Id", "Name", aomFieldMeta.AomMetaId);
            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name", aomFieldMeta.FieldTypeId);
            return View(aomFieldMeta);
        }

        // POST: Admin/AomFieldMetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AomMetaId,Name,FieldTypeId,Display")] AomFieldMeta aomFieldMeta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aomFieldMeta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AomMetaId = new SelectList(db.AomMetas, "Id", "Name", aomFieldMeta.AomMetaId);
            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name", aomFieldMeta.FieldTypeId);
            return View(aomFieldMeta);
        }

        // GET: Admin/AomFieldMetas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomFieldMeta aomFieldMeta = db.AomFieldMetas.Find(id);
            if (aomFieldMeta == null)
            {
                return HttpNotFound();
            }
            return View(aomFieldMeta);
        }

        // POST: Admin/AomFieldMetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AomFieldMeta aomFieldMeta = db.AomFieldMetas.Find(id);
            db.AomFieldMetas.Remove(aomFieldMeta);
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
