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
    public class RelationshipMetasController : Controller
    {
        private AomDbContext db = new AomDbContext();

        // GET: Admin/RelationshipMetas
        public ActionResult Index()
        {
            var relationshipMetas = db.RelationshipMetas.Include(r => r.FkAomFieldMeta).Include(r => r.FkAomMeta).Include(r => r.PkAomFieldMeta).Include(r => r.PkAomMeta);
            return View(relationshipMetas.ToList());
        }

        // GET: Admin/RelationshipMetas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelationshipMeta relationshipMeta = db.RelationshipMetas.Find(id);
            if (relationshipMeta == null)
            {
                return HttpNotFound();
            }
            return View(relationshipMeta);
        }

        // GET: Admin/RelationshipMetas/Create
        public ActionResult Create()
        {
            ViewBag.FkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name");
            ViewBag.FkAomMetaId = new SelectList(db.AomMetas, "Id", "Name");
            ViewBag.PkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name");
            ViewBag.PkAomMetaId = new SelectList(db.AomMetas, "Id", "Name");
            return View();
        }

        // POST: Admin/RelationshipMetas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PkAomMetaId,FkAomMetaId,FkAomFieldMetaId,Name,PkAomFieldMetaId")] RelationshipMeta relationshipMeta)
        {
            if (ModelState.IsValid)
            {
                relationshipMeta.Id = Guid.NewGuid();
                db.RelationshipMetas.Add(relationshipMeta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name", relationshipMeta.FkAomFieldMetaId);
            ViewBag.FkAomMetaId = new SelectList(db.AomMetas, "Id", "Name", relationshipMeta.FkAomMetaId);
            ViewBag.PkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name", relationshipMeta.PkAomFieldMetaId);
            ViewBag.PkAomMetaId = new SelectList(db.AomMetas, "Id", "Name", relationshipMeta.PkAomMetaId);
            return View(relationshipMeta);
        }

        // GET: Admin/RelationshipMetas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelationshipMeta relationshipMeta = db.RelationshipMetas.Find(id);
            if (relationshipMeta == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name", relationshipMeta.FkAomFieldMetaId);
            ViewBag.FkAomMetaId = new SelectList(db.AomMetas, "Id", "Name", relationshipMeta.FkAomMetaId);
            ViewBag.PkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name", relationshipMeta.PkAomFieldMetaId);
            ViewBag.PkAomMetaId = new SelectList(db.AomMetas, "Id", "Name", relationshipMeta.PkAomMetaId);
            return View(relationshipMeta);
        }

        // POST: Admin/RelationshipMetas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PkAomMetaId,FkAomMetaId,FkAomFieldMetaId,Name,PkAomFieldMetaId")] RelationshipMeta relationshipMeta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relationshipMeta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name", relationshipMeta.FkAomFieldMetaId);
            ViewBag.FkAomMetaId = new SelectList(db.AomMetas, "Id", "Name", relationshipMeta.FkAomMetaId);
            ViewBag.PkAomFieldMetaId = new SelectList(db.AomFieldMetas, "Id", "Name", relationshipMeta.PkAomFieldMetaId);
            ViewBag.PkAomMetaId = new SelectList(db.AomMetas, "Id", "Name", relationshipMeta.PkAomMetaId);
            return View(relationshipMeta);
        }

        // GET: Admin/RelationshipMetas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RelationshipMeta relationshipMeta = db.RelationshipMetas.Find(id);
            if (relationshipMeta == null)
            {
                return HttpNotFound();
            }
            return View(relationshipMeta);
        }

        // POST: Admin/RelationshipMetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            RelationshipMeta relationshipMeta = db.RelationshipMetas.Find(id);
            db.RelationshipMetas.Remove(relationshipMeta);
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
