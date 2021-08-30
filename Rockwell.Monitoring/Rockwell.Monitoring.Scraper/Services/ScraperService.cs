using Rockwell.Monitoring.Database;
using Rockwell.Monitoring.Database.Entities;
using Rockwell.Monitoring.Scraper.Models.Requests;
using Rockwell.Monitoring.Scraper.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rockwell.Monitoring.Scraper.Services
{
    public class ScraperService : IScraperService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly MonitoringContext _monitoringContext;

        public ScraperService (IHttpClientFactory clientFactory, MonitoringContext monitoringContext)
        {
            _clientFactory = clientFactory;
            _monitoringContext = monitoringContext;
        }

        public async Task ExecuteScrape(ScrapeRequest request)
        {
            ExecutionResult result = await ScrapeUrl(request);
            _monitoringContext.ExecutionResults.Add(result);
            _monitoringContext.SaveChanges(true);
        }

        private async Task<ExecutionResult> ScrapeUrl(ScrapeRequest request)
        {
            ExecutionResult result;
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync(request.Url);
                int statusCode = (int)response.StatusCode;
                string content = await response.Content.ReadAsStringAsync();
                result = new ExecutionResult
                {
                    ScrapeJobId = request.ScrapeJobId,
                    ExecutionTime = DateTime.Now,
                    ResponseCode = statusCode,
                    Result = content.Substring(0, 1000)
                };
            }
            catch (Exception e)
            {
                result = new ExecutionResult
                {
                    ScrapeJobId = request.ScrapeJobId,
                    ExecutionTime = DateTime.Now,
                    ErrorMessage = e.Message.Substring(0, 500)
                };
            }
            return result;
        }
    }
}
