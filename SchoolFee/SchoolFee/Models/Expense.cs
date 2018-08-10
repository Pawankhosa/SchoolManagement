using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolFee.Models
{
    public class Expense
    {
        [Key]
        public int EId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name ="Expense Name")]
        public string ExpenseName { get; set; } 
        [Display(Name ="Type")]
        public int ETID { get; set; } 
        public virtual ExpenseType ExpenseTypes { get; set; } 
        public string TypeDetail { get; set; } 
        [Required]
        public string Amount { get; set; } 
     

    }
}