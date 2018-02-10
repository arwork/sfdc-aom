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
    public class FieldTypesController : Controller
    {
        private AomDbContext db = new AomDbContext();

        // GET: Admin/FieldTypes
        public ActionResult Index()
        {
            return View(db.FieldTypes.ToList());
        }

        // GET: Admin/FieldTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldType fieldType = db.FieldTypes.Find(id);
            if (fieldType == null)
            {
                return HttpNotFound();
            }
            return View(fieldType);
        }

        // GET: Admin/FieldTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/FieldTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] FieldType fieldType)
        {
            if (ModelState.IsValid)
            {
                fieldType.Id = Guid.NewGuid();
                db.FieldTypes.Add(fieldType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fieldType);
        }

        // GET: Admin/FieldTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldType fieldType = db.FieldTypes.Find(id);
            if (fieldType == null)
            {
                return HttpNotFound();
            }
            return View(fieldType);
        }

        // POST: Admin/FieldTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] FieldType fieldType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fieldType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fieldType);
        }

        // GET: Admin/FieldTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldType fieldType = db.FieldTypes.Find(id);
            if (fieldType == null)
            {
                return HttpNotFound();
            }
            return View(fieldType);
        }

        // POST: Admin/FieldTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            FieldType fieldType = db.FieldTypes.Find(id);
            db.FieldTypes.Remove(fieldType);
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
