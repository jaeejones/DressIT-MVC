using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DressIt.Models;

namespace DressIt.Controllers
{
    public class WardrobeItemsController : Controller
    {
        private DressItDBEntities db = new DressItDBEntities();

        // GET: WardrobeItems
        public ActionResult Index()
        {
            var wardrobeItems = db.WardrobeItems.Include(w => w.Type);
            return View(wardrobeItems.ToList());
        }

        // GET: WardrobeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardrobeItem wardrobeItem = db.WardrobeItems.Find(id);
            if (wardrobeItem == null)
            {
                return HttpNotFound();
            }
            return View(wardrobeItem);
        }

        // GET: WardrobeItems/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.Types, "TypeID", "TypeName");
            return View();
        }

        // POST: WardrobeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WardrobeItemID,TypeID,Name,Photo")] WardrobeItem wardrobeItem)
        {
            if (ModelState.IsValid)
            {
                db.WardrobeItems.Add(wardrobeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.Types, "TypeID", "TypeName", wardrobeItem.TypeID);
            return View(wardrobeItem);
        }

        // GET: WardrobeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardrobeItem wardrobeItem = db.WardrobeItems.Find(id);
            if (wardrobeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.Types, "TypeID", "TypeName", wardrobeItem.TypeID);
            return View(wardrobeItem);
        }

        // POST: WardrobeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WardrobeItemID,TypeID,Name,Photo")] WardrobeItem wardrobeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wardrobeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeID = new SelectList(db.Types, "TypeID", "TypeName", wardrobeItem.TypeID);
            return View(wardrobeItem);
        }

        // GET: WardrobeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardrobeItem wardrobeItem = db.WardrobeItems.Find(id);
            if (wardrobeItem == null)
            {
                return HttpNotFound();
            }
            return View(wardrobeItem);
        }

        // POST: WardrobeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WardrobeItem wardrobeItem = db.WardrobeItems.Find(id);
            db.WardrobeItems.Remove(wardrobeItem);
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
