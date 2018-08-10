using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Month
    {
        [Key]
        public int MID { get; set; } 
        [Required]
        public string Months { get; set; } 
    }
}