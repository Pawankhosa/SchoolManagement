using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolFee.Models
{
    public class ExpenseType
    {
        [Key]
        public int ETID { get; set; } 
        [Required]
        public string Type { get; set; } 
        public virtual ICollection<Expense> Expenses { get; set; }

    }
}