using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJCollegeMVC.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Please Enter your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter your Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please Enter your Phone Number")]
        [RegularExpression(@"\D{10}",ErrorMessage ="Please enter 10 digit Mobile No")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Enter your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter your Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please Enter your Role")]
        public string Role { get; set; }
        public string Approval { get; set; }
        [Required(ErrorMessage = "Please Enter your Gender")]
        public string Gender { get; set; }
    }
}