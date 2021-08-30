using Rockwell.Monitoring.ExecutionsApi.Models.Responses;
using System.Collections.Generic;

namespace Rockwell.Monitoring.ExecutionsApi.Services.Interfaces
{
    public interface IExecutionsService
    {
        IEnumerable<ExecutionInfo> GetAll();
        IEnumerable<ExecutionInfo> GetByJob(long jobId);
        ExecutionData GetById(long id);
    }
}
