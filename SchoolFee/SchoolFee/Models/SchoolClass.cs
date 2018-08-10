using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class SchoolClass
    {
        [Key]
        public int CID { get; set; } 
        [Required(ErrorMessage ="Please Enter Class Name")]
        public string ClassName { get; set; } 
        public virtual ICollection<FeeModule> FeeModules { get; set; }
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}