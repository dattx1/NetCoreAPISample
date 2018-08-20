using BusinessAccess.NotificationPayload;
using Newtonsoft.Json;
using NotificationLayer.FCM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationLayer.Config
{
    public class MessageGenerator
    {
        public enum Flatform
        {
            GCM = 1,
            APNS,
            APNS_SANDBOX
        }
        public static string getPlatformMessage(NotificationParameter param)
        {
            switch (param.Flatform)
            {
                case Flatform.GCM:
                    return getAndroidMessage(param);
                case Flatform.APNS:
                    return getAppledMessage(param);
                case Flatform.APNS_SANDBOX:
                    return getAppledMessage(param);
                default:
                    throw new Exception("Platform is not supported: " + Enum.GetName(typeof(Flatform), param.Flatform));

            }
        }
        public static string getAndroidMessage(NotificationParameter parameters)
        {
            Dictionary<string, object> androidMessageDic = new Dictionary<string, object>();
            Dictionary<string, object> notification = new Dictionary<string, object>();
            Dictionary<string, object> dataPayload = new Dictionary<string, object>();

            notification.Add("title", parameters.Title);
            notification.Add("body", parameters.Body);
            notification.Add("click_action", parameters.Payload.ClickActionId);
            dataPayload.Add("customKey", parameters.Payload);

            if (parameters.RegistrationIds.Count < 2)
                androidMessageDic.Add("to", parameters.RegistrationIds.FirstOrDefault());
            else
                androidMessageDic.Add("registration_ids", parameters.RegistrationIds.ToArray());

            androidMessageDic.Add("mutable_content", true);
            androidMessageDic.Add("data", dataPayload);
            androidMessageDic.Add("notification", notification);

            return JsonConvert.SerializeObject(androidMessageDic);

        }

        public static string getAppledMessage(NotificationParameter parameters)
        {
            Dictionary<string, object> notification = new Dictionary<string, object>();
            Dictionary<string, object> appMessageDic = new Dictionary<string, object>();
            Dictionary<string, object> appdataDic = new Dictionary<string, object>();

            appdataDic.Add("data", parameters.Payload);
            notification.Add("title", parameters.Title);
            notification.Add("body", "Ấn vào để xem");
            notification.Add("sound", "adcmover_notify_sound.m4r");
            notification.Add("mutable_content", true);
            notification.Add("badge", 1);


            appMessageDic.Add("priority", "high");
            appMessageDic.Add("notification", notification);
            if (parameters.RegistrationIds.Count < 2)
                appMessageDic.Add("to", parameters.RegistrationIds.FirstOrDefault());
            else
                appMessageDic.Add("registration_ids", parameters.RegistrationIds.ToArray());
            appMessageDic.Add("data", appdataDic);

            return JsonConvert.SerializeObject(appMessageDic);
        }

    }
}
