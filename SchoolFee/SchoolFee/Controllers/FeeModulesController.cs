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
    public class FeeModulesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: FeeModules
        public async Task<ActionResult> Index()
        {
            var feeModules = db.FeeModules.Include(f => f.SchoolClasses);
            return View(await feeModules.ToListAsync());
        }

        // GET: FeeModules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeModule feeModule = await db.FeeModules.FindAsync(id);
            if (feeModule == null)
            {
                return HttpNotFound();
            }
            return View(feeModule);
        }

        // GET: FeeModules/Create
        public ActionResult Create()
        {
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName");
            return View();
        }

        // POST: FeeModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FID,CID,Fee,AnnualCharges")] FeeModule feeModule)
        {
            if (ModelState.IsValid)
            {
                db.FeeModules.Add(feeModule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", feeModule.CID);
            return View(feeModule);
        }

        // GET: FeeModules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeModule feeModule = await db.FeeModules.FindAsync(id);
            if (feeModule == null)
            {
                return HttpNotFound();
            }
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", feeModule.CID);
            return View(feeModule);
        }

        // POST: FeeModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FID,CID,Fee,AnnualCharges")] FeeModule feeModule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feeModule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", feeModule.CID);
            return View(feeModule);
        }

        // GET: FeeModules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeeModule feeModule = await db.FeeModules.FindAsync(id);
            if (feeModule == null)
            {
                return HttpNotFound();
            }
            return View(feeModule);
        }

        // POST: FeeModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FeeModule feeModule = await db.FeeModules.FindAsync(id);
            db.FeeModules.Remove(feeModule);
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
