using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DealerShipMVCApp.Models;

namespace DealerShipMVCApp.Controllers
{
    public class VehiclesController : Controller
    {
        private VehicleDBContext db = new VehicleDBContext();

        // GET: Vehicles
       // [Authorize (Roles = "Admin, CanEnterIndex" )]
        public ActionResult Index(string searchString, string vehicleModel, string vehicleColor, string vehiclePriceRangeStart, string vehiclePriceRangeEnd)
        {
            //string searchString = Request.QueryString["searchString"];
            //string Rating = Request.QueryString["Rating"];
            //string Genre = Request.QueryString["Genre"];

            //var vehicleQry = from m in db.Vehicles
            //               where ((string.IsNullOrEmpty(searchString) ? true : m.Make == searchString) &&
            //                      (string.IsNullOrEmpty(vehicleModel) ? true : m.Model == vehicleModel) &&
            //                      (string.IsNullOrEmpty(vehicleColor) ? true : m.Color == vehicleColor))
            //               select m;

            var vehicles = from m in db.Vehicles
                           select m;

            var ModelLst = new List<string>();
            var ModelQry = from d in db.Vehicles
                           orderby d.Model
                           select d.Model;
            ModelLst.AddRange(ModelQry.Distinct());
            ViewBag.vehicleModel = new SelectList(ModelLst);

            var ColorLst = new List<string>();
            var ColorQry = from f in db.Vehicles
                           orderby f.Color
                           select f.Color;
            ColorLst.AddRange(ColorQry.Distinct());
            ViewBag.vehicleColor = new SelectList(ColorLst);


            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s => s.Make.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(vehicleModel))
            {
                vehicles = vehicles.Where(x => x.Model == vehicleModel);
            }
            if (!string.IsNullOrEmpty(vehicleColor))
            {
                vehicles = vehicles.Where(x => x.Color == vehicleColor);
            }            

           // if (!string.IsNullOrEmpty(vehiclePriceRangeStart))
           //{
           //     double PriceRangeStart = Convert.ToDouble(vehiclePriceRangeStart);
           //     vehicles = vehicles.Where(x => x.MSRP >= PriceRangeStart);
           //     if(!string.IsNullOrEmpty(vehiclePriceRangeEnd))
           //     {
           //         double PriceRangeEnd = Convert.ToDouble(vehiclePriceRangeEnd);
           //         vehicles = vehicles.Where(x => x.MSRP <= PriceRangeEnd);
           //     }
           // }

            return View(vehicles); 
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Make,Model,Year,Color,MilePerGallon,MSRP")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Make,Model,Year,Color,MilePerGallon,MSRP")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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