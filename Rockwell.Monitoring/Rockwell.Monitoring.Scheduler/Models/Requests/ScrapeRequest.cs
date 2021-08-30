namespace Rockwell.Monitoring.Scheduler.Models.Requests
{
    public class ScrapeRequest
    {
        public long ScrapeJobId { get; set; }
        public string Url { get; set; }
    }
}
