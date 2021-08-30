using Quartz;
using Rockwell.Monitoring.Scheduler.Jobs;
using Rockwell.Monitoring.Scheduler.Models;
using Rockwell.Monitoring.Scheduler.Services.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rockwell.Monitoring.Scheduler.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduler _scheduler;

        public ScheduleService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task ScheduleScrapeJob (ScrapeJobInfo jobInfo)
        {
            var job = CreateJob(jobInfo);
            var trigger = CreateTrigger(jobInfo);
            await _scheduler.ScheduleJob(job, trigger);
        }

        private static IJobDetail CreateJob(ScrapeJobInfo jobInfo) =>
            JobBuilder
                .Create(typeof(ScrapeJob))
                .WithIdentity(jobInfo.ScrapeJobId.ToString())
                .WithDescription(JsonSerializer.Serialize(jobInfo))
                .Build();

        private static ITrigger CreateTrigger(ScrapeJobInfo jobInfo) => 
            TriggerBuilder
                .Create()
                .WithIdentity($"{jobInfo.ScrapeJobId}.trigger")
                .WithCronSchedule(jobInfo.Cron)
                .WithDescription(JsonSerializer.Serialize(jobInfo))
                .Build();
    }
}
