using System.Collections.Generic;
using MvcProject.Models;

namespace MvcProject.Infrastructure
{
    public class UserIdComparer : IEqualityComparer<UserViewModel>
    {
        public bool Equals(UserViewModel x, UserViewModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(UserViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}