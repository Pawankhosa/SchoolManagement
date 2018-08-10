using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolFee.Models;
using Admin2.Models;
using SchoolManagement.Models;
namespace SchoolFee.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        dbcontext db = new dbcontext();
        Registration rr = new Registration();
        
        
        public ActionResult Index(int? id)
        {
            var regno = db.MasterFees.Where(x => x.BillNo == id).FirstOrDefault().RID;
            TempData["RID"] = regno;
            TempData["Bill"] = id;
            return View(db.MasterFees.Where(x=>x.BillNo==id).ToList());
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Invoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
