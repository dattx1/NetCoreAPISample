using NotificationLayer.Config;
using NotificationLayer.FCM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLayer
{
    public interface IFCMService
    {
        Task SendNotification(NotificationParameter parameter);
    }
}
