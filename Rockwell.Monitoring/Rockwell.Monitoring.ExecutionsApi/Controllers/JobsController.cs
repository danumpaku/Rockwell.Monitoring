using Microsoft.AspNetCore.Mvc;
using Rockwell.Monitoring.ExecutionsApi.Models.Requests;
using Rockwell.Monitoring.ExecutionsApi.Models.Responses;
using Rockwell.Monitoring.ExecutionsApi.Services.Interfaces;
using System.Collections.Generic;

namespace Rockwell.Monitoring.ExecutionsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobsService _jobsService;

        public JobsController(IJobsService jobsService)
        {
            _jobsService = jobsService;
        }

        [HttpGet]
        public IEnumerable<ScrapeJobInfo> GetAll()
        {
            return _jobsService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<ScrapeJobInfo> GetById(long id)
        {
            ScrapeJobInfo job = _jobsService.GetById(id);
            if (job != null)
                return Ok(job);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<ScrapeJobCreationResponse> Create(ScrapeJobCreationRequest request)
        {
            var response = _jobsService.Create(request);
            if (response.ErrorMessage == null)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
