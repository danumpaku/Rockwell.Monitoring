using Quartz;
using Rockwell.Monitoring.Scheduler.Models;
using Rockwell.Monitoring.Scheduler.Models.Requests;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rockwell.Monitoring.Scheduler.Jobs
{
    public class ScrapeJob : IJob
    {
        private readonly ScrapeJobInfo _jobInfo;
        private readonly IHttpClientFactory _clientFactory;

        public ScrapeJob(ScrapeJobInfo jobInfo, IHttpClientFactory clientFactory)
        {
            _jobInfo = jobInfo;
            _clientFactory = clientFactory;
        }

        public Task Execute(IJobExecutionContext context)
        {
            HttpClient client = _clientFactory.CreateClient("scraper");
            var ans = client.PostAsync("scrape", GetScrapeRequest()).Result;
            return Task.CompletedTask;
        }

        private HttpContent GetScrapeRequest()
        {
            ScrapeRequest request = new ScrapeRequest
            {
                ScrapeJobId = _jobInfo.ScrapeJobId,
                Url = _jobInfo.Url
            };
            string jsonRequest = JsonSerializer.Serialize(request);
            return new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        }
    }
}
