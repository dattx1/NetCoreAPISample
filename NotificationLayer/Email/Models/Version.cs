using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLayer.Email.Models
{
    public class Version
    {
        public string id { get; set; }
        public string template_id { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
    }
}
