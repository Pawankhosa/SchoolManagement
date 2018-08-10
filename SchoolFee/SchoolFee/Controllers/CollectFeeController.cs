using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin2.Models;
using SchoolManagement.Models;
using SchoolManagement.Controllers;

namespace SchoolFee.Controllers
{
    public class CollectFeeController : Controller
    {
        dbcontext db = new dbcontext();
        Helper help = new Helper();
        // GET: CollectFee
        public ActionResult Index()
        {
            string a = help.EncryptData("FuTureViSion2013");
            Registration reg = new Registration();
            return View(db.Registrations.Where(x=>x.AddmissionNumber==null).ToList());
        }
        [HttpPost]
        public ActionResult Index(int classname,int sectionname,Registration reg)
        {
            return View(db.Registrations.Where(x=>x.CID==classname && x.SCID==sectionname).ToList());
        }

        // GET: CollectFee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollectFee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollectFee/Create
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

        // GET: CollectFee/Edit/5
        public ActionResult Edit(int id)
        {
            return RedirectToAction("Index", "FeeDetail", new { id = id });
        }

        // POST: CollectFee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index","FeeDetail", new { id=id});
            }
            catch
            {
                return View();
            }
        }

        // GET: CollectFee/Delete/5
        public ActionResult PayNow(int id)
        {
            return RedirectToAction("PayNow", "FeeDetail", new { id = id });
        }

        // POST: CollectFee/Delete/5
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
