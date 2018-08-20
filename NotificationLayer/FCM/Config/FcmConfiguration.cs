using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLayer.Config
{
    public class FcmConfiguration
    {
        private const string FCM_SEND_URL = "https://fcm.googleapis.com/fcm/send";

        public FcmConfiguration(IConfiguration Configuration, string optionalApplicationIdPackageName = "")
        {
            this.SenderAuthToken = Configuration["FCM:SenderAuthToken"];
            if (!string.IsNullOrWhiteSpace(optionalApplicationIdPackageName))
                this.ApplicationIdPackageName = optionalApplicationIdPackageName;
            this.FcmUrl = FCM_SEND_URL;

            this.ValidateServerCertificate = false;
        }

        public FcmConfiguration(string senderAuthToken, string optionalApplicationIdPackageName = "")
        {
            this.SenderAuthToken = senderAuthToken;
            if (!string.IsNullOrWhiteSpace(optionalApplicationIdPackageName))
                this.ApplicationIdPackageName = optionalApplicationIdPackageName;
            this.FcmUrl = FCM_SEND_URL;

            this.ValidateServerCertificate = false;
        }

        public string SenderID { get; private set; }

        public string SenderAuthToken { get; private set; }

        public string ApplicationIdPackageName { get; private set; }

        public bool ValidateServerCertificate { get; set; }

        public string FcmUrl { get; set; }

        public void OverrideUrl(string url)
        {
            FcmUrl = url;
        }
    }
}
