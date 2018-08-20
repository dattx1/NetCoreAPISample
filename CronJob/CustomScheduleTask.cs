using AppWithScheduler.Code;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CronJob
{
    public class CustomScheduleTask : IScheduledTask
    {
        private readonly IConfiguration _config;

        public CustomScheduleTask(IConfiguration config)
        {
            _config = config;
        }

        public string Schedule => "* * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.GetAsync(_config["CronJobAPI"].ToString());
            }
        }
    }
}
