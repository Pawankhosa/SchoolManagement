using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Section
    {
        [Key]
        public int SCID { get; set; } 
        [Required(ErrorMessage ="Pleas Enter Sesction Name")]
        public string SectionName { get; set; } 
        public virtual ICollection<Registration> Registrations { get; set; }

    }
}