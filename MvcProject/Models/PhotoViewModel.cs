using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class PhotoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        [Display(Name = "Total rate")]
        public int TotalRate { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
    }
}