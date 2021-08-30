using Rockwell.Monitoring.Database;
using Rockwell.Monitoring.ExecutionsApi.Models.Responses;
using Rockwell.Monitoring.ExecutionsApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Rockwell.Monitoring.ExecutionsApi.Services
{
    public class ExecutionsService : IExecutionsService
    {
        private readonly MonitoringContext _monitoringContext;

        public ExecutionsService(MonitoringContext monitoringContext)
        {
            _monitoringContext = monitoringContext;
        }

        public IEnumerable<ExecutionInfo> GetAll()
        {
            return _monitoringContext.ExecutionResults
                .Select(er => new ExecutionInfo
                {
                    ExecutionId = er.Id,
                    ExecutionTime = er.ExecutionTime,
                    ScrapeJobId = er.ScrapeJobId,
                    Url = er.ScrapeJob.Url,
                    CronExpression = er.ScrapeJob.CronExpression,
                    ResponseCode = er.ResponseCode,
                    HasErrors = er.ErrorMessage != null
                });
        }

        public IEnumerable<ExecutionInfo> GetByJob(long jobId)
        {
            return _monitoringContext.ExecutionResults
                .Where(er => er.ScrapeJobId == jobId)
                .Select(er => new ExecutionInfo
                {
                    ExecutionId = er.Id,
                    ExecutionTime = er.ExecutionTime,
                    ScrapeJobId = er.ScrapeJobId,
                    Url = er.ScrapeJob.Url,
                    CronExpression = er.ScrapeJob.CronExpression,
                    ResponseCode = er.ResponseCode,
                    HasErrors = er.ErrorMessage != null
                });
        }

        public ExecutionData GetById(long id)
        {
            return _monitoringContext.ExecutionResults
               .Where(er => er.Id == id)
               .Select(er => new ExecutionData
               {
                   ExecutionId = er.Id,
                   ExecutionTime = er.ExecutionTime,
                   ResponseCode = er.ResponseCode,
                   Result = er.Result,
                   ErrorMessage = er.ErrorMessage
               }).SingleOrDefault();
        }
    }
}
