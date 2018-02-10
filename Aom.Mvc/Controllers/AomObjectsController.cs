using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CustomLogic;

namespace Aom.Mvc.Controllers
{
    public class AomObjectsController : Controller
    {
        private AomDbContext db = new AomDbContext();

        // GET: AomObjects
        public ActionResult Index(Guid? id, int amount = 10, int page = 1, string query = "")
        {
            // Menu Stuff list of AomMeta to make the menu from
            ViewBag.AomMetas = db.AomMetas.Take(10).ToList();
            ViewBag.ObjectType = id?.ToString();
            ViewBag.Query = query?.ToString();
            IQueryable<AomObject> tableData = db.AomObjects;

            // Table stuff filtering sorting etc
            if (id != null)
            {
                tableData = tableData.Where(c => c.AomMetaId == id); // filter by type if there is an id
                
            }
            
            if (!string.IsNullOrEmpty(query)) // filter buy query
            {
                var AomFieldObjects = db.AomFieldObjects.Where(c => c.Value.Contains(query));
                tableData = tableData.Where(c => AomFieldObjects.Select(d => d.AomObjectId).Contains(c.Id));
                tableData = tableData.Where(c => c.Name.Contains(query)); // search name

                // this will crash
                //tableData = tableData.Where(c => c.Name.Contains(query) ||
                // c.AomFieldObjects.Any(cc => cc.Value.Contains(query))); // serach values
            }

            ViewBag.TotalRecords = tableData.Count();
           // var viewModel = tableData.OrderBy(c => c.Id).Skip((page - 1) * amount).Take(amount).ToList();

            return View(tableData.OrderBy(c => c.Id).Skip((page - 1) * amount).Take(amount).ToList());
        }

        // GET: AomObjects/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomObject aomObject = db.AomObjects.Find(id);
            if (aomObject == null)
            {
                return HttpNotFound();
            }
            return View(aomObject);
        }

        // GET: AomObjects/Create/AomMetaId
        public ActionResult Create(Guid id)
        {
            AomObject viewModel = new AomObject();
            viewModel.AomMeta = db.AomMetas.FirstOrDefault(c => c.Id == id); // used to display the type
            viewModel.AomMetaId = viewModel.AomMeta.Id;
            viewModel.AomFieldObjects = viewModel.AomMeta.AomFieldMetas.Select(c => new AomFieldObject { AomFieldMetaId = c.Id, AomFieldMeta = c}).ToList();

            // now make all the drop donw lists
            foreach (var aomFieldMeta in viewModel.AomMeta.AomFieldMetas)
            {
                if (aomFieldMeta.Name.Contains("Id"))
                {
                    // Get a list of object that match the 
                    var testMe = db.RelationshipMetas.FirstOrDefault(cc => cc.FkAomFieldMetaId == aomFieldMeta.Id);
                    if (testMe != null)
                    {
                        var dropDownObjects = db.AomObjects.Where(c => c.AomMetaId == testMe.PkAomMetaId).ToList();
                        ViewData[aomFieldMeta.Name] = dropDownObjects;
                    }
                }
            }
            
            return View(viewModel);
        }

        // POST: AomObjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AomObject aomObject)
        {
            aomObject.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                db.AomObjects.Add(aomObject);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aomObject);
        }

        // GET: AomObjects/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomObject aomObject = db.AomObjects.Find(id);
            if (aomObject == null)
            {
                return HttpNotFound();
            }

            // fill up the ViewData with the Id and Display Name for the linkable objects if any 
            var otherKeys = db.RelationshipMetas.Where(c => c.FkAomMetaId == aomObject.AomMetaId);
            return View(aomObject);
        }

        // POST: AomObjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AomObject aomObject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aomObject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aomObject);
        }

        // GET: AomObjects/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AomObject aomObject = db.AomObjects.Find(id);
            if (aomObject == null)
            {
                return HttpNotFound();
            }
            return View(aomObject);
        }

        // POST: AomObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AomObject aomObject = db.AomObjects.Find(id);
            db.AomObjects.Remove(aomObject);
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
