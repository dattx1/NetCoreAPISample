using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLayer.Config
{
    public class FcmServiceConnection
    {
        public FcmServiceConnection(FcmConfiguration configuration)
        {
            Configuration = configuration;
            http = new HttpClient();

            http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + Configuration.SenderAuthToken);

        }

        public FcmConfiguration Configuration { get; private set; }

        readonly HttpClient http;

        public async Task Send(string notification)
        {
            var content = new StringContent(notification, System.Text.Encoding.UTF8, "application/json");
            http.DefaultRequestHeaders.TryAddWithoutValidation("content-length", notification.Length.ToString());
            var response = await http.PostAsync(Configuration.FcmUrl, content);
        }
    }
}
