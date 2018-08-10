using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin2.Models;
using SchoolFee.Models;
using SchoolManagement.Controllers;
using SchoolManagement.Models;

namespace SchoolFee.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        dbcontext db = new dbcontext();
        public ActionResult BillWise()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BillWise(MonthlyFee Fee,int billno)
        {
            Fee = db.MonthlyFees.FirstOrDefault(x => x.BillNo ==billno);
            if (Fee != null)
            {
                return RedirectToAction("Index", "Invoice", new { id = Fee.BillNo });
            }
            else {
                this.SetNotification("Invalid Bill No", NotificationEnumeration.Error);
                return View();
            }
        }
        public ActionResult EditByAd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditByAd(int AddmissionNumber, Registration Reg)
        {
            Reg = db.Registrations.FirstOrDefault(x => x.AddmissionNumber == AddmissionNumber);
            if (Reg != null)
            {
                return RedirectToAction("RegNo", "Registrations", new { id = AddmissionNumber });
            }
            else {
                this.SetNotification("Invalid Admission Number", NotificationEnumeration.Error);
                return View();
            }
           
        }

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
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

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
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

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
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
