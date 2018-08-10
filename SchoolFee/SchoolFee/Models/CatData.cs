using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class CatData
    {
        [Key]
        public int CatID { get; set; } 
        [Required]
        public string CategoryName { get; set; } 
        public virtual ICollection<Registration> Registrations { get; set; }

    }
}