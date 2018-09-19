using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class UserType : BaseEntity
    {
        public UserType()
        {
            UserTypeUser = new HashSet<UserTypeUser>();
        }

        public string UserTypeName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserTypeUser> UserTypeUser { get; set; }
    }
}
