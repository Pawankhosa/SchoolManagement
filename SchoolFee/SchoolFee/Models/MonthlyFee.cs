using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolFee.Models
{
    public class MonthlyFee
    {
        [Key]
        public int MonthId { get; set; } 
        
        public string Date { get; set; } 
        public int BillNo { get; set; } 
        public int RID { get; set; }
        public string session { get; set; }
        public double TotalFee { get; set; } 
        public double Paid { get; set; }
        public double Discount { get; set; }
        public string Reason { get; set; }
        public double Other { get; set; }
        public double Balance { get; set; } 
        public string TimePeriod { get; set; } 
        public string status { get; set; } 
    }
}