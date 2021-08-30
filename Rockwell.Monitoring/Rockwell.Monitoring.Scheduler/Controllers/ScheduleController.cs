using Microsoft.AspNetCore.Mvc;
using Quartz;
using Rockwell.Monitoring.Scheduler.Models;
using Rockwell.Monitoring.Scheduler.Models.Requests;
using Rockwell.Monitoring.Scheduler.Services.Interfaces;
using System;

namespace Rockwell.Monitoring.Scheduler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public ActionResult ScheduleScrapeJob(ScheduleRequest request)
        {
            ScrapeJobInfo jobInfo = new ScrapeJobInfo
            {
                ScrapeJobId = request.ScrapeJobId,
                Url = request.Url,
                Cron = request.Cron
            };
            try
            {
                _scheduleService.ScheduleScrapeJob(jobInfo);
                return Ok();
            }
            catch (FormatException e)
            {
                return BadRequest(e.Message);
            }
            catch (ObjectAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
