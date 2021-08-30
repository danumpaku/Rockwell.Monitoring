using Quartz;
using Quartz.Spi;
using Rockwell.Monitoring.Scheduler.Jobs;
using Rockwell.Monitoring.Scheduler.Models;
using System.Net.Http;
using System.Text.Json;

namespace Rockwell.Monitoring.Scheduler.JobFactories
{
    public class ScrapeJobFactory : IJobFactory
    {
        private readonly IHttpClientFactory _clientFactory;

        public ScrapeJobFactory (IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jsonJobInfo = bundle.JobDetail.Description;
            ScrapeJobInfo jobInfo = JsonSerializer.Deserialize<ScrapeJobInfo>(jsonJobInfo);
            return new ScrapeJob(jobInfo, _clientFactory);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
