using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class FeeModule
    {
        [Key]
        public int FID { get; set; } 
        public int CID { get; set; } 
        public virtual SchoolClass SchoolClasses { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public Double Fee { get; set; }
        [DataType(DataType.Currency)]
        public Double AnnualCharges { get; set; }
    }
}