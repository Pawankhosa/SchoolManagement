using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class TransportCharges
    {
        [Key]
        public int TID { get; set; } 
        [Required(ErrorMessage ="Please Enter Area")]
        public string AreaName { get; set; } 
        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; } 
        public virtual ICollection<Registration> Registrations { get; set; }
    }
}