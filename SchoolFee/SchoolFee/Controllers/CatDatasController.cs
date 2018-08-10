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
    public class CatDatasController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: CatDatas
        public async Task<ActionResult> Index()
        {
            return View(await db.CatDatas.ToListAsync());
        }

        // GET: CatDatas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatData catData = await db.CatDatas.FindAsync(id);
            if (catData == null)
            {
                return HttpNotFound();
            }
            return View(catData);
        }

        // GET: CatDatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CatID,CategoryName")] CatData catData)
        {
            if (ModelState.IsValid)
            {
                db.CatDatas.Add(catData);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(catData);
        }

        // GET: CatDatas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatData catData = await db.CatDatas.FindAsync(id);
            if (catData == null)
            {
                return HttpNotFound();
            }
            return View(catData);
        }

        // POST: CatDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CatID,CategoryName")] CatData catData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catData).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(catData);
        }

        // GET: CatDatas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CatData catData = await db.CatDatas.FindAsync(id);
            if (catData == null)
            {
                return HttpNotFound();
            }
            return View(catData);
        }

        // POST: CatDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CatData catData = await db.CatDatas.FindAsync(id);
            db.CatDatas.Remove(catData);
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
