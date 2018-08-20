using System;
using System.Collections.Generic;
using System.Text;

namespace Security.SecurityModel
{
    public class current_user_access
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> UserType { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
