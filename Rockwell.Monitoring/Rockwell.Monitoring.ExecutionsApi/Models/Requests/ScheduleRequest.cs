namespace Rockwell.Monitoring.ExecutionsApi.Models.Requests
{
    public class ScheduleRequest
    {
        public long ScrapeJobId { get; set; }
        public string Url { get; set; }
        public string Cron { get; set; }
    }
}
