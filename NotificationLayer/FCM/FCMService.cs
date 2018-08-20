using BusinessAccess.NotificationPayload;
using Microsoft.Extensions.Configuration;
using NotificationLayer.Config;
using NotificationLayer.FCM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLayer
{
    public class FCMService : IFCMService
    {
        private FcmServiceConnection connection;

        public FCMService(IConfiguration Configuration)
        {
            connection = new FcmServiceConnection(new FcmConfiguration(Configuration));
        }

        public async Task SendNotification(NotificationParameter parameter)
        {
            var payLoad = string.Empty;
            switch (parameter.Flatform)
            {
                case MessageGenerator.Flatform.GCM:
                    payLoad = MessageGenerator.getAndroidMessage(parameter);
                    break;
                case MessageGenerator.Flatform.APNS_SANDBOX:
                    payLoad = MessageGenerator.getAppledMessage(parameter);
                    break;
                case MessageGenerator.Flatform.APNS:
                    payLoad = MessageGenerator.getAppledMessage(parameter);
                    break;
                default:
                    break;
            };

            await connection.Send(payLoad);
        }
    }
}
