using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class FeeStructure
    {
        [Key]
        public int FeeID { get; set; } 
        public int RID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public double AdmissionFee { get; set; } 
        public double TotalFee { get; set; } 
        public double TransportFee { get; set; }
        public double AnnualCharges { get; set; }
        public double OtherCharges { get; set; }
        public double Discount { get; set; }
        public double Pay { get; set; }
        public double Fine { get; set; } 
        public string Status { get; set; } 
        public string Session { get; set; } 

    }
}