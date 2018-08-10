using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin2.Models;
using SchoolManagement.Models;

namespace SchoolFee.Controllers
{
    public class TransportChargesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: TransportCharges
        public async Task<ActionResult> Index()
        {
            return View(await db.TransportCharges.ToListAsync());
        }

        // GET: TransportCharges/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportCharges transportCharges = await db.TransportCharges.FindAsync(id);
            if (transportCharges == null)
            {
                return HttpNotFound();
            }
            return View(transportCharges);
        }

        // GET: TransportCharges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransportCharges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TID,AreaName,Amount")] TransportCharges transportCharges)
        {
            if (ModelState.IsValid)
            {
                db.TransportCharges.Add(transportCharges);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(transportCharges);
        }

        // GET: TransportCharges/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportCharges transportCharges = await db.TransportCharges.FindAsync(id);
            if (transportCharges == null)
            {
                return HttpNotFound();
            }
            return View(transportCharges);
        }

        // POST: TransportCharges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TID,AreaName,Amount")] TransportCharges transportCharges)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportCharges).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transportCharges);
        }

        // GET: TransportCharges/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportCharges transportCharges = await db.TransportCharges.FindAsync(id);
            if (transportCharges == null)
            {
                return HttpNotFound();
            }
            return View(transportCharges);
        }

        // POST: TransportCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TransportCharges transportCharges = await db.TransportCharges.FindAsync(id);
            db.TransportCharges.Remove(transportCharges);
            await db.SaveChangesAsync();
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
