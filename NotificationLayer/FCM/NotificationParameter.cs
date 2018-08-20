using BusinessAccess.NotificationPayload;
using NotificationLayer.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLayer.FCM
{
    public class NotificationParameter
    {
        public NotificationParameter()
        {
            RegistrationIds = new List<string>();
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public PayloadBase Payload { get; set; }
        public MessageGenerator.Flatform Flatform { get; set; }
        public List<string> RegistrationIds { get; set; }
        public int IconType { get; set; }
    }
}
