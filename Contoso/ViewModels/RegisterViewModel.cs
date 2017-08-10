using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contoso.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("User Name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 8)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        [MaxLength(150)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        [MaxLength(150)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please select your State Location")]
        [Display(Name = "State")]
        public string State { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }


    }
}