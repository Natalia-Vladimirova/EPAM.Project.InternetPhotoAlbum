using System;
using System.ComponentModel.DataAnnotations;

namespace MvcProject.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

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

        public byte[] UserPhoto { get; set; }
    }
}