using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class Photo
    {
        public int PhotoId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public int TotalRate { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public Photo()
        {
            Ratings = new HashSet<Rating>();
        }
    }
}
