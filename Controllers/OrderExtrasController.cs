using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proiect_Rent_A_Car.Models;

namespace Proiect_Rent_A_Car.Controllers
{
    public class OrderExtrasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderExtras
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.OrderExtras.ToList());
        }

        // GET: OrderExtras/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderExtras orderExtras = db.OrderExtras.Find(id);
            if (orderExtras == null)
            {
                return HttpNotFound();
            }
            return View(orderExtras);
        }

        // GET: OrderExtras/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderExtras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,OrderId,ExtrasId")] OrderExtras orderExtras)
        {
            if (ModelState.IsValid)
            {
                db.OrderExtras.Add(orderExtras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderExtras);
        }

        // GET: OrderExtras/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderExtras orderExtras = db.OrderExtras.Find(id);
            if (orderExtras == null)
            {
                return HttpNotFound();
            }
            return View(orderExtras);
        }

        // POST: OrderExtras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ExtrasId")] OrderExtras orderExtras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderExtras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderExtras);
        }

        // GET: OrderExtras/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderExtras orderExtras = db.OrderExtras.Find(id);
            if (orderExtras == null)
            {
                return HttpNotFound();
            }
            return View(orderExtras);
        }

        // POST: OrderExtras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderExtras orderExtras = db.OrderExtras.Find(id);
            db.OrderExtras.Remove(orderExtras);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
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
