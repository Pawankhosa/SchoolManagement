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
    public class MonthsController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: Months
        public async Task<ActionResult> Index()
        {
            return View(await db.Months.ToListAsync());
        }

        // GET: Months/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Month month = await db.Months.FindAsync(id);
            if (month == null)
            {
                return HttpNotFound();
            }
            return View(month);
        }

        // GET: Months/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Months/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MID,Months")] Month month)
        {
            if (ModelState.IsValid)
            {
                db.Months.Add(month);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(month);
        }

        // GET: Months/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Month month = await db.Months.FindAsync(id);
            if (month == null)
            {
                return HttpNotFound();
            }
            return View(month);
        }

        // POST: Months/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MID,Months")] Month month)
        {
            if (ModelState.IsValid)
            {
                db.Entry(month).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(month);
        }

        // GET: Months/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Month month = await db.Months.FindAsync(id);
            if (month == null)
            {
                return HttpNotFound();
            }
            return View(month);
        }

        // POST: Months/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Month month = await db.Months.FindAsync(id);
            db.Months.Remove(month);
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
