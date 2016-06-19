using System.ComponentModel.DataAnnotations;

namespace MvcProject.Models
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "The field can not be empty.")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewPasswordConfirm { get; set; }
    }
}