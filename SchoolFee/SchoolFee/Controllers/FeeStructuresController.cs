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
    public class FeeStructuresController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: FeeStructures
        public async Task<ActionResult> Index()
        {
           // var registrations = db.FeeStructures.Include(r => r.RID).Include(r => r.SchoolClasses).Include(r => r.Sections).Include(r => r.TransportCharges);
            return View(await db.FeeStructures.ToListAsync());
        }

        // GET: FeeStructures/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeStructure feeStructure = await db.FeeStructures.FindAsync(id);
            if (feeStructure == null)
            {
                return HttpNotFound();
            }
            return View(feeStructure);
        }

        // GET: FeeStructures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeeStructures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FeeID,RID,AdmissionFee,TotalFee,TransportFee,AnnualCharges,OtherCharges,Status,Session")] FeeStructure feeStructure)
        {
            if (ModelState.IsValid)
            {
                db.FeeStructures.Add(feeStructure);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(feeStructure);
        }

        // GET: FeeStructures/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeStructure feeStructure = await db.FeeStructures.FindAsync(id);
            if (feeStructure == null)
            {
                return HttpNotFound();
            }
            return View(feeStructure);
        }

        // POST: FeeStructures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FeeID,RID,AdmissionFee,TotalFee,TransportFee,AnnualCharges,OtherCharges,Status,Session")] FeeStructure feeStructure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feeStructure).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(feeStructure);
        }

        // GET: FeeStructures/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeStructure feeStructure = await db.FeeStructures.FindAsync(id);
            if (feeStructure == null)
            {
                return HttpNotFound();
            }
            return View(feeStructure);
        }

        // POST: FeeStructures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            FeeStructure feeStructure = await db.FeeStructures.FindAsync(id);
            db.FeeStructures.Remove(feeStructure);
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
