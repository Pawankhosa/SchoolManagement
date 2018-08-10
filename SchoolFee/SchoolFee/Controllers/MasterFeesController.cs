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
using SchoolFee.Models;

namespace SchoolFee.Controllers
{
    public class MasterFeesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: MasterFees
        public async Task<ActionResult> Index()
        {
            return View(await db.MasterFees.ToListAsync());
        }

        // GET: MasterFees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterFee masterFee = await db.MasterFees.FindAsync(id);
            if (masterFee == null)
            {
                return HttpNotFound();
            }
            return View(masterFee);
        }

        // GET: MasterFees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MasterId,Date,BillNo,RID,session,TotalFee,Paid,Discount,Reason,Other,Balance,TimePeriod,status")] MasterFee masterFee)
        {
            if (ModelState.IsValid)
            {
                db.MasterFees.Add(masterFee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(masterFee);
        }

        // GET: MasterFees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterFee masterFee = await db.MasterFees.FindAsync(id);
            if (masterFee == null)
            {
                return HttpNotFound();
            }
            return View(masterFee);
        }

        // POST: MasterFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MasterId,Date,BillNo,RID,session,TotalFee,Paid,Discount,Reason,Other,Balance,TimePeriod,status")] MasterFee masterFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masterFee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(masterFee);
        }

        // GET: MasterFees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasterFee masterFee = await db.MasterFees.FindAsync(id);
            if (masterFee == null)
            {
                return HttpNotFound();
            }
            return View(masterFee);
        }

        // POST: MasterFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MasterFee masterFee = await db.MasterFees.FindAsync(id);
            db.MasterFees.Remove(masterFee);
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
