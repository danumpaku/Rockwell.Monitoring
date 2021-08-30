using System;

namespace Rockwell.Monitoring.ExecutionsApi.Models.Responses
{
    public class ExecutionInfo
    {
        public long ExecutionId { get; set; }
        public DateTime ExecutionTime { get; set; }
        public long ScrapeJobId { get; set; }
        public string Url { get; set; }
        public string CronExpression { get; set; }
        public int ResponseCode { get; set; }
        public bool HasErrors { get; set; }
    }
}
