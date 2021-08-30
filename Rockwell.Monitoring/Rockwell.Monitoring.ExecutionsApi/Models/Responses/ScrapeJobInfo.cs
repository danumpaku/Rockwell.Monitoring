using System;

namespace Rockwell.Monitoring.ExecutionsApi.Models.Responses
{
    public class ScrapeJobInfo
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Url { get; set; }
        public string CronExpression { get; set; }
    }
}
