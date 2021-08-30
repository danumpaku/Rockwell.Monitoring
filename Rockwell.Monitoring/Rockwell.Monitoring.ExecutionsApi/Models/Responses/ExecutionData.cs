using System;

namespace Rockwell.Monitoring.ExecutionsApi.Models.Responses
{
    public class ExecutionData
    {
        public long ExecutionId { get; set; }
        public DateTime ExecutionTime { get; set; }
        public int ResponseCode { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
