using Microsoft.EntityFrameworkCore;
using Rockwell.Monitoring.Database;
using Rockwell.Monitoring.Database.Entities;
using Rockwell.Monitoring.ExecutionsApi.Models.Requests;
using Rockwell.Monitoring.ExecutionsApi.Models.Responses;
using Rockwell.Monitoring.ExecutionsApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rockwell.Monitoring.ExecutionsApi.Services
{
    public class JobsService : IJobsService
    {
        private readonly MonitoringContext _monitoringContext;
        private readonly IHttpClientFactory _clientFactory;

        public JobsService(MonitoringContext monitoringContext, IHttpClientFactory clientFactory)
        {
            _monitoringContext = monitoringContext;
            _clientFactory = clientFactory;
        }

        public IEnumerable<ScrapeJobInfo> GetAll()
        {
            return _monitoringContext.ScrapeJobs
                .Select(st => new ScrapeJobInfo
                {
                    Id = st.Id,
                    CreationTime = st.CreationTime,
                    CronExpression = st.CronExpression,
                    Url = st.Url
                });
        }

        public ScrapeJobInfo GetById(long id)
        {
            return _monitoringContext.ScrapeJobs
                .Where(st => st.Id == id)
                .Select(st => new ScrapeJobInfo
                {
                    Id = st.Id,
                    CreationTime = st.CreationTime,
                    CronExpression = st.CronExpression,
                    Url = st.Url
                }).SingleOrDefault();
        }

        public ScrapeJobCreationResponse Create(ScrapeJobCreationRequest request)
        {
            ScrapeJobCreationResponse response;
            try
            {
                ScrapeJob job = new ScrapeJob
                {
                    Url = request.Url,
                    CronExpression = request.CronExpression,
                    CreationTime = DateTime.Now
                };
                
                _monitoringContext.Add(job);
                _monitoringContext.SaveChanges(true);

                ScheduleJob(job);

                response = new ScrapeJobCreationResponse
                {
                    JobInfo = new ScrapeJobInfo
                    {
                        Id = job.Id,
                        CreationTime = job.CreationTime,
                        Url = job.Url,
                        CronExpression = job.CronExpression
                    }
                };

            }
            catch (DbUpdateException e)
            {
                response = new ScrapeJobCreationResponse { ErrorMessage = e.Message };
            }

            return response;
        }

        private Task<HttpResponseMessage> ScheduleJob (ScrapeJob job)
        {
            HttpClient client = _clientFactory.CreateClient("scheduler");
            ScheduleRequest scheduleRequest = new ScheduleRequest
            {
                ScrapeJobId = job.Id,
                Url = job.Url,
                Cron = job.CronExpression
            };
            string jsonRequest = JsonSerializer.Serialize(scheduleRequest);
            return client.PostAsync("schedule", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
        }
    }
}
