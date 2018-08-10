using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolFee.Models
{
    public class User
    {

        public int id { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter User Name")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [StringLength(255, MinimumLength = 5)]
        public string Password { get; set; }
        public string Role { get; set; } 
    }
}