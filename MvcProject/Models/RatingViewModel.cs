using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class RatingViewModel
    {
        public int RatingId { get; set; }
        public int UserRate { get; set; }
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
    }
}