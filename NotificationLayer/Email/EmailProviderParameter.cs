using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLayer
{
    public class EmailProviderParameter
    {
        public string mail_from_title { get; set; }
        public string mail_from { get; set; }
        public string mail_subject { get; set; }
        public string mail_to_title { get; set; }
        public string mail_to { get; set; }
        public Dictionary<string, string> mail_tos { get; set; }
        public Dictionary<string, string> merge_fields { get; set; }
        public string template_id { get; set; }
        public string plain_text_content { get; set; }
        public string html_content { get; set; }
    }
}
