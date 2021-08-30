using Rockwell.Monitoring.ExecutionsApi.Models.Requests;
using Rockwell.Monitoring.ExecutionsApi.Models.Responses;
using System.Collections.Generic;

namespace Rockwell.Monitoring.ExecutionsApi.Services.Interfaces
{
    public interface IJobsService
    {
        IEnumerable<ScrapeJobInfo> GetAll();
        ScrapeJobInfo GetById(long id);
        ScrapeJobCreationResponse Create(ScrapeJobCreationRequest request);
    }
}
