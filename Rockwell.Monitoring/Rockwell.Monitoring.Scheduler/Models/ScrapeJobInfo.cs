namespace Rockwell.Monitoring.Scheduler.Models
{
    public class ScrapeJobInfo
    {
        public long ScrapeJobId { get; set; }
        public string Url { get; set; }
        public string Cron { get; set; }
    }
}
