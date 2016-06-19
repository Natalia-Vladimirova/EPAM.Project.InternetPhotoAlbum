using System.Collections.Generic;

namespace MvcProject.Models
{
    public class UsersViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}