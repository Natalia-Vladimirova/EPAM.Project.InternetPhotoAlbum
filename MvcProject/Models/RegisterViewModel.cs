using System;
using System.ComponentModel.DataAnnotations;

namespace MvcProject.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The field can not be empty.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
    }
}