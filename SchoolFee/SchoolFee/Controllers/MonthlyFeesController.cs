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
    public class MonthlyFeesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: MonthlyFees
        public async Task<ActionResult> Index()
        {
            return View(await db.MonthlyFees.ToListAsync());
        }

        // GET: MonthlyFees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyFee monthlyFee = await db.MonthlyFees.FindAsync(id);
            if (monthlyFee == null)
            {
                return HttpNotFound();
            }
            return View(monthlyFee);
        }

        // GET: MonthlyFees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonthlyFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MonthId,Date,BillNo,RID,session,TotalFee,Paid,Balance,TimePeriod,status")] MonthlyFee monthlyFee)
        {
            if (ModelState.IsValid)
            {
                db.MonthlyFees.Add(monthlyFee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(monthlyFee);
        }

        // GET: MonthlyFees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyFee monthlyFee = await db.MonthlyFees.FindAsync(id);
            if (monthlyFee == null)
            {
                return HttpNotFound();
            }
            return View(monthlyFee);
        }

        // POST: MonthlyFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MonthId,Date,BillNo,RID,session,TotalFee,Paid,Balance,TimePeriod,status")] MonthlyFee monthlyFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlyFee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(monthlyFee);
        }

        // GET: MonthlyFees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlyFee monthlyFee = await db.MonthlyFees.FindAsync(id);
            if (monthlyFee == null)
            {
                return HttpNotFound();
            }
            return View(monthlyFee);
        }

        // POST: MonthlyFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MonthlyFee monthlyFee = await db.MonthlyFees.FindAsync(id);
            db.MonthlyFees.Remove(monthlyFee);
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
