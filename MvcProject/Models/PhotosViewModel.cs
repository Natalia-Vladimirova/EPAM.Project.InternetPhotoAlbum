using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class PhotosViewModel
    {
        public UserViewModel ChosenUser { get; set; }
        public IEnumerable<PhotoViewModel> Photos { get; set; }
        public PhotoViewModel CurrentPhoto { get; set; }
        public IEnumerable<RatingViewModel> CurrentPhotoRatings { get; set; }
        public int? RatingOfCurrentUser { get; set; }
    }
}