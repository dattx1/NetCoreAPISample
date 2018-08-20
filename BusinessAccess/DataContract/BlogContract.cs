using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccess.DataContract
{
    public class BlogContract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
