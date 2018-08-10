using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class Registration
    {
        [Key]
        public int REGId { get; set; }
        public string Session { get; set; }
     
        public string Type { get; set; }
        [Required]
        public int AddmissionNumber { get; set; }
        public int RollNumber { get; set; }
        [Required]
        public int CID { get; set; } // Class
        public virtual SchoolClass SchoolClasses { get; set; } 
        public int SCID { get; set; } // Section
        public virtual Section Sections { get; set; } 
        [Required]
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Gender { get; set; } 
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; } 
        public int CatID { get; set; } //Category
        public virtual CatData CatDatas { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } 
        [DataType(DataType.Date)]
        public DateTime AdmissionDate { get; set; } 
        public string Image { get; set; }
        //Gardian Details
        [Required]
        public string FatherName { get; set; } 
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string FatherPhone { get; set; } 
        public string FatherOccupation { get; set; } 
        public string MotherName { get; set; } 
        [DataType(DataType.PhoneNumber)]
        public string MotherPhone { get; set; } 
        public string MotherOccupation { get; set; } 
        public string CurrentAddress { get; set; } 
        public string ParmanentAddress { get; set; } 
        [MinLength(1)][MaxLength(12)]
        public string AadharNumber { get; set; } 
        public int TID { get; set; } 
        public virtual TransportCharges TransportCharges { get; set; }

        public Boolean SpecialCase { get; set; } 
        public string Remarks { get; set; } 




    }
    public enum Gender
    {
        Male,
        Female
    }
    public enum Type
    {
        New,
        Old
    }
}