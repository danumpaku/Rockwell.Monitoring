using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using Rockwell.Monitoring.Scheduler.Jobs;
using Rockwell.Monitoring.Scheduler.Models;

namespace Rockwell.Monitoring.Scheduler.Services
{
    public class QuartzHostedService : IHostedService
    {
        private readonly IScheduler _scheduler;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<ScrapeJobInfo> _jobSchedules;

        public QuartzHostedService(
            IScheduler scheduler,
            IJobFactory jobFactory,
            IEnumerable<ScrapeJobInfo> jobSchedules)
        {
            _scheduler = scheduler;
            _jobFactory = jobFactory;
            _jobSchedules = jobSchedules;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler.JobFactory = _jobFactory;

            foreach (var jobInfo in _jobSchedules)
            {
                var job = CreateJob(jobInfo);
                var trigger = CreateTrigger(jobInfo);

                await _scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await _scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJob(ScrapeJobInfo jobInfo)
        {
            var jobType = typeof(ScrapeJob);
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobInfo.Url)
                .Build();
        }

        private static ITrigger CreateTrigger(ScrapeJobInfo jobInfo)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{typeof(ScrapeJob).FullName}.trigger")
                .WithCronSchedule(jobInfo.Cron)
                .WithDescription(jobInfo.Url)
                .Build();
        }
    }
}
