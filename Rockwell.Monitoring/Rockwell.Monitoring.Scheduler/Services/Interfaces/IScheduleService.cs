using Rockwell.Monitoring.Scheduler.Models;
using System.Threading.Tasks;

namespace Rockwell.Monitoring.Scheduler.Services.Interfaces
{
    public interface IScheduleService
    {
        Task ScheduleScrapeJob(ScrapeJobInfo jobInfo);
    }
}
