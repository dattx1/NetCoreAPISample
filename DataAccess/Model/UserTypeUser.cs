using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class UserTypeUser : BaseEntity
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }

        public virtual User User { get; set; }

        public virtual UserType UserType { get; set; }
    }
}
