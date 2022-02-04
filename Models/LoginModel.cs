using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJCollegeMVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Please Enter your Name")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please Enter your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Approval { get; set; }
        public string Batch { get; set; }
        public string Id { get; set; }

    }
}