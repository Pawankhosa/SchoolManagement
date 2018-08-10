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
    public class SchoolClassesController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: SchoolClasses
        public async Task<ActionResult> Index()
        {
            return View(await db.SchoolClasses.ToListAsync());
        }

        // GET: SchoolClasses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = await db.SchoolClasses.FindAsync(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CID,ClassName")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                db.SchoolClasses.Add(schoolClass);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(schoolClass);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = await db.SchoolClasses.FindAsync(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            return View(schoolClass);
        }

        // POST: SchoolClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CID,ClassName")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolClass).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(schoolClass);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolClass schoolClass = await db.SchoolClasses.FindAsync(id);
            if (schoolClass == null)
            {
                return HttpNotFound();
            }
            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SchoolClass schoolClass = await db.SchoolClasses.FindAsync(id);
            db.SchoolClasses.Remove(schoolClass);
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
