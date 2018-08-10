using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Session
    {
        [Key]
        public int SID { get; set; } 
        public string SessionName { get; set; } 
        public bool Status { get; set; } 

    }
}