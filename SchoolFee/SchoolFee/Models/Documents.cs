using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Documents
    {
        [Key]
        public int DOCID { get; set; }
        public string StudentId { get; set; }  
        public string Doc1 { get; set; } 
        public string Doc2 { get; set; } 
        public string Doc3 { get; set; } 
        public string Doc4 { get; set; } 
        public string Doc5 { get; set; } 

    }
}