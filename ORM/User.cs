using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public byte[] UserPhoto { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public User()
        {
            Roles = new HashSet<Role>();
            Ratings = new HashSet<Rating>();
            Photos = new HashSet<Photo>();
        }
    }
}
