using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proiect_Rent_A_Car.Models;
using Microsoft.AspNet.Identity;

namespace Proiect_Rent_A_Car.Controllers
{
    public class ListingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static int listingId;
        // GET: Listings
        [Authorize]
        public ActionResult Index()
        {
            var listings = db.Listings.Include(l => l.Car);
            if (User.IsInRole("Admin"))
            {
                foreach (var ls in listings)
                {
                    ls.IsAdmin = true;
                }
            }
            else
            {
                foreach (var ls in listings)
                {
                    ls.IsAdmin = false;
                }
            }
            return View(listings.ToList());
        }

        // GET: Listings/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        public ActionResult OrderConfirmation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        public ActionResult InvalidDates()
        {
            return View();
        }
        // GET: Listings/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Make");
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,CarId,Price,WarrantyCost,Description")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Listings.Add(listing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "Id", "Make", listing.CarId);
            return View(listing);
        }
        [Authorize]
        public ActionResult MakeOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listingId = int.Parse(id.ToString());
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            Order order = new Order
            {
                ListingId = listing.Id,
                Listing = listing
            };
            var extras = db.Extras.ToList();

            ViewBag.Extras = extras;
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Make");
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult MakeOrder([Bind(Include = "Id,PickUpDate,ReturnDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                if(!(DateTime.Now < order.PickUpDate && DateTime.Now < order.ReturnDate && order.PickUpDate < order.ReturnDate))
                    return RedirectToAction($"InvalidDates");
                var totalDays = (order.ReturnDate - order.PickUpDate).TotalDays;
                var allUsers = db.Users.ToList();
                var agents = new List<ApplicationUser>();
                foreach (var user in allUsers)
                {
                    foreach (var role in user.Roles)
                    {
                        if (role.RoleId == "2")
                        {
                            agents.Add(user);
                            break;
                        }
                    }
                }

                Random rnd = new Random();
                int r = rnd.Next(agents.Count);
                var agent = agents[r];

                order.AgentId = agent.Id; 
                order.ListingId = listingId;
                order.Listing = db.Listings.FirstOrDefault(x => x.Id == listingId);
                order.TotalPrice = order.Listing.Price * int.Parse(Math.Floor(totalDays).ToString());
                order.UserId = User.Identity.GetUserId();
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction($"OrderConfirmation/{order.Id}");
            }

            return View(order);
        }

        // GET: Listings/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Make", listing.CarId);
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,CarId,Price,WarrantyCost,Description")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "Id", "Make", listing.CarId);
            return View(listing);
        }

        // GET: Listings/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Listing listing = db.Listings.Find(id);
            db.Listings.Remove(listing);
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
