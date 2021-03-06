﻿using System.ComponentModel.DataAnnotations;

namespace MvcProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field can not be empty.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}