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
using System.Transactions;
using SchoolManagement.Controllers;
using System.Data.Entity.Validation;

namespace SchoolFee.Controllers
{
    public class RegistrationsController : BaseController
    {
        private dbcontext db = new dbcontext();

        // GET: Registrations
        public async Task<ActionResult> Index()
        {
            var registrations = db.Registrations.Include(r => r.CatDatas).Include(r => r.SchoolClasses).Include(r => r.Sections).Include(r => r.TransportCharges);
            return View(await registrations.ToListAsync());
        }

        // GET: Registrations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.CatID = new SelectList(db.CatDatas, "CatID", "CategoryName");
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName");
            ViewBag.SCID = new SelectList(db.Sections, "SCID", "SectionName");
            ViewBag.TID = new SelectList(db.TransportCharges, "TID", "AreaName");
            Registration rr = new Registration();
            rr.AdmissionDate = System.DateTime.Now;
            return View(rr);
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "REGId,Session,Type,AddmissionNumber,RollNumber,CID,SCID,FirstName,LastName,Gender,DOB,CatID,Mobile,Email,AdmissionDate,Image,FatherName,FatherPhone,FatherOccupation,MotherName,MotherPhone,MotherOccupation,CurrentAddress,ParmanentAddress,AadharNumber,TID,SpecialCase,Remarks")] Registration registration,string admission)
        {
            if (ModelState.IsValid)
            {
                
                    
                    try
                    {
                        var check = db.Registrations.Where(x => x.AddmissionNumber == registration.AddmissionNumber).FirstOrDefault();
                        if (check!=null)
                        {
                            this.SetNotification("Admission Number Busy", NotificationEnumeration.Error);
                            ViewBag.CatID = new SelectList(db.CatDatas, "CatID", "CategoryName", registration.CatID);
                            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", registration.CID);
                            ViewBag.SCID = new SelectList(db.Sections, "SCID", "SectionName", registration.SCID);
                            ViewBag.TID = new SelectList(db.TransportCharges, "TID", "AreaName", registration.TID);
                            return View();
                        }
                        else
                        {

                            db.Registrations.Add(registration);
                            await db.SaveChangesAsync();
                            FeeModule sm = db.FeeModules.Where(x => x.CID == registration.CID).FirstOrDefault();
                            TransportCharges tc = db.TransportCharges.Where(x => x.TID == registration.TID).FirstOrDefault();
                            if (sm != null)
                            {
                                #region Fee Structure
                                FeeStructure fs = new FeeStructure();
                                fs.Date = registration.AdmissionDate;
                                fs.AdmissionFee =Convert.ToDouble(admission);
                                fs.AnnualCharges = sm.AnnualCharges;
                                fs.TotalFee = (sm.Fee * Convert.ToDouble(12));
                                fs.TransportFee = (tc.Amount * Convert.ToDouble(12));
                                fs.RID =Convert.ToInt32(registration.AddmissionNumber);
                                fs.Status = "Pending";
                                fs.Session = Session["session"].ToString();
                                fs.AnnualCharges = 0;
                                fs.OtherCharges = 0;
                                fs.Pay = 0;
                                
                                fs.Discount = 0.0;
                                fs.Fine = 0.0;
                                db.FeeStructures.Add(fs);
                                db.SaveChanges();
                                #endregion



                            }
                        }
                       
                    }
                    catch (DbEntityValidationException dbEx) 
                    {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }

                    throw;
                    }
                
                return RedirectToAction("Index");
            }

            ViewBag.CatID = new SelectList(db.CatDatas, "CatID", "CategoryName", registration.CatID);
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", registration.CID);
            ViewBag.SCID = new SelectList(db.Sections, "SCID", "SectionName", registration.SCID);
            ViewBag.TID = new SelectList(db.TransportCharges, "TID", "AreaName", registration.TID);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.CatDatas, "CatID", "CategoryName", registration.CatID);
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", registration.CID);
            ViewBag.SCID = new SelectList(db.Sections, "SCID", "SectionName", registration.SCID);
            ViewBag.TID = new SelectList(db.TransportCharges, "TID", "AreaName", registration.TID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "REGId,Session,Type,AddmissionNumber,RollNumber,CID,SCID,FirstName,LastName,Gender,DOB,CatID,Mobile,Email,AdmissionDate,Image,FatherName,FatherPhone,FatherOccupation,MotherName,MotherPhone,MotherOccupation,CurrentAddress,ParmanentAddress,AadharNumber,TID,SpecialCase,Remarks")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.CatDatas, "CatID", "CategoryName", registration.CatID);
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", registration.CID);
            ViewBag.SCID = new SelectList(db.Sections, "SCID", "SectionName", registration.SCID);
            ViewBag.TID = new SelectList(db.TransportCharges, "TID", "AreaName", registration.TID);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = await db.Registrations.FindAsync(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Registration registration = await db.Registrations.FindAsync(id);
            db.Registrations.Remove(registration);
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
        public async Task<ActionResult> RegNo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reg = db.Registrations.Where(x => x.AddmissionNumber == id).FirstOrDefault().REGId;
            Registration registration = await db.Registrations.FindAsync(reg);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatID = new SelectList(db.CatDatas, "CatID", "CategoryName", registration.CatID);
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", registration.CID);
            ViewBag.SCID = new SelectList(db.Sections, "SCID", "SectionName", registration.SCID);
            ViewBag.TID = new SelectList(db.TransportCharges, "TID", "AreaName", registration.TID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegNo([Bind(Include = "REGId,Session,Type,AddmissionNumber,RollNumber,CID,SCID,FirstName,LastName,Gender,DOB,CatID,Mobile,Email,AdmissionDate,Image,FatherName,FatherPhone,FatherOccupation,MotherName,MotherPhone,MotherOccupation,CurrentAddress,ParmanentAddress,AadharNumber,TID,SpecialCase,Remarks")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.CatDatas, "CatID", "CategoryName", registration.CatID);
            ViewBag.CID = new SelectList(db.SchoolClasses, "CID", "ClassName", registration.CID);
            ViewBag.SCID = new SelectList(db.Sections, "SCID", "SectionName", registration.SCID);
            ViewBag.TID = new SelectList(db.TransportCharges, "TID", "AreaName", registration.TID);
            return View(registration);
        }

    }
}
