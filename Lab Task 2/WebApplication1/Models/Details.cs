using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //for validating with annotation
using WebApplication1.Attributes;

namespace WebApplication1.Models
{
    public class Details
    {
        [Required(ErrorMessage ="Please provide your name")]
        [StringLength(50,MinimumLength = 3)]
        [RegularExpression(@"^([A-Za-z][A-Za-z_.\s]{3,50})$")]        
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Enter your student ID")]
        [RegularExpression(@"^([0-9]{2}[-][0-9]{5}[-][0-3]{1})$", ErrorMessage ="Id should be like 'XX-XXXXX-X'")]
        public string Id { get; set; }
        
        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage ="Email Required")]        
        [RegularExpression(@"^[0-9]{2}[-][0-9]{5}[-][0-3]{1}@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Invalid Email. It should be like 'XX-XXXXX-X@student.aiub.edu'")]         
        public string email { get; set; }
        
        [Required(ErrorMessage ="Password Cannot be Empty")]
        [StringLength(32,MinimumLength =8)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,32}$",ErrorMessage ="Password Must Contain one Uppercase, Lowercase, Special Character, Number")]
        public string password { get; set; }

        [Compare("password",ErrorMessage ="Password does not match")]
        public string confPassword { get; set; }

        [AgeValidation(ErrorMessage ="Age must 18 or higher")]
        public string DOB { get; set; }

       
    }
}