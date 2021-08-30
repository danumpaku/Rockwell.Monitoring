namespace Rockwell.Monitoring.ExecutionsApi.Models.Requests
{
    public class ScrapeJobCreationRequest
    {
        public string Url { get; set; }
        public string CronExpression { get; set; }
    }
}
