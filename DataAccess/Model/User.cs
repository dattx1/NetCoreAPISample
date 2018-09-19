using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class User : BaseEntity
    {
        public User()
        {
            UserTypeUser = new HashSet<UserTypeUser>();
        }

        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserTypeUser> UserTypeUser { get; set; }
    }
}
