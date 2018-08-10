using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin2.Models;
using System.Data.SqlClient;
using System.Dynamic;
using SchoolManagement.Models;
using SchoolFee.Models;
using System.Data.Entity;

namespace SchoolFee.Controllers
{
    public class FeeDetailController : Controller
    {
        dbcontext db = new dbcontext();
      //  SqlDataReader rr = new SqlDataReader();
        // GET: FeeDetail
        public ActionResult Index(int id)
        {
            dynamic model = new ExpandoObject();
            string year = Session["session"].ToString();
            model.Registration = db.Registrations.Where(x => x.AddmissionNumber == id);
            model.MonthlyDetail = db.MonthlyFees.Where(x => x.RID == id && x.session == year);


            return View(model);
        }

        // GET: FeeDetail/Details/5
        public ActionResult Reprint(int id)
        {
            return RedirectToAction("Index", "Invoice", new { id = id });
        }

        // GET: FeeDetail/Create
        public ActionResult PayNow(int id)
        {
            dynamic mymodel = new ExpandoObject();
            Registration rr = db.Registrations.FirstOrDefault(x=>x.AddmissionNumber==id);
            if (rr != null)
            {
                mymodel.FeeModule = db.FeeModules.Where(x => x.CID == rr.CID);
                mymodel.Registration = db.Registrations.Where(x => x.AddmissionNumber == id);
                mymodel.Month = db.Months.ToList();
                mymodel.monthlyfee = db.MonthlyFees.Where(x => x.RID == id);
            }

            TempData["Class"] = rr.CID.ToString();
            int mf = db.MonthlyFees.Max(x => x.BillNo);
            if (mf <= 0)
            {
                TempData["Bill"] = 1;

            }
            else
            {
                TempData["Bill"] = mf+1;
            }

            return View(mymodel);
        }

        // POST: FeeDetail/Create
        [HttpPost]
        public ActionResult Fee(string bill,string date,string totalvalue, string annual,string transport,string pay,string balance,string period,int regno,string discount,string reason,string other,MonthlyFee Month,FeeStructure fee,MasterFee Master)
        {
            try
            {
                string[] array = new string[12];
                array = period.Split(',');
                if (array.Length > 0)
                {
                    double total = (Convert.ToDouble(totalvalue) / Convert.ToDouble(array.Length));
                    double paymonthly = (Convert.ToDouble(pay) / Convert.ToDouble(array.Length));
                    double paybal = (Convert.ToDouble(balance) / Convert.ToDouble(array.Length));
                    double paydiscount= (Convert.ToDouble(discount) / Convert.ToDouble(array.Length));
                    double payother = (Convert.ToDouble(other) / Convert.ToDouble(array.Length));
                    for (int i = 1; i <= array.Length; i++)
                    {
                        int a = i - 1;
                        Month.Date = date;
                        Month.BillNo = Convert.ToInt32(bill);
                        Month.TimePeriod = array[a];
                        Month.Paid = paymonthly;
                        Month.TotalFee =total;
                        Month.session = Session["session"].ToString();
                        Month.Balance = paybal;
                        Month.status = "Paid";
                        Month.Discount = paydiscount;
                        Month.Reason = reason;
                        Month.Other = payother;
                        Month.RID = regno;
                        db.MonthlyFees.Add(Month);
                        db.SaveChanges();
                    }
                    Master.Date = date;
                    Master.BillNo = Convert.ToInt32(bill);
                    Master.TimePeriod = period;
                    Master.Paid = Convert.ToDouble(pay);
                    Master.TotalFee = Convert.ToDouble(totalvalue); 
                    Master.session = Session["session"].ToString();
                    Master.Balance = Convert.ToDouble(balance); 
                    Master.status = "Paid";
                    Master.Discount = Convert.ToDouble(discount); 
                    Master.Reason = reason;
                    Master.Other = Convert.ToDouble(other); 
                    Master.RID = regno;
                    db.MasterFees.Add(Master);
                    db.SaveChanges();

                } 
                
                var id = db.FeeStructures.Where(x => x.RID == regno).FirstOrDefault().FeeID;
                fee = db.FeeStructures.Find(id);
                if (fee != null)
                {
                    fee.Pay = (Convert.ToDouble(fee.Pay) + Convert.ToDouble(pay));
                    fee.Discount = (Convert.ToDouble(fee.Discount) + Convert.ToDouble(discount));
                    fee.AnnualCharges = (Convert.ToDouble(fee.AnnualCharges) + Convert.ToDouble(annual));
                    //fee.AdmissionFee = (Convert.ToDouble(fee.Discount) + Convert.ToDouble(discount));
                    db.Entry(fee).State = EntityState.Modified;
                   
                    db.SaveChanges();


                }
                
                
                



                return RedirectToAction("Index","Invoice", new {id=bill });
            }
            catch
            {
                return View();
            }
        }

        // GET: FeeDetail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeeDetail/Edit/5
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

        // GET: FeeDetail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeeDetail/Delete/5
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
