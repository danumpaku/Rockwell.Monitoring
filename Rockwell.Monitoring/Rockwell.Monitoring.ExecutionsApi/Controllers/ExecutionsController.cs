using Microsoft.AspNetCore.Mvc;
using Rockwell.Monitoring.ExecutionsApi.Models.Responses;
using Rockwell.Monitoring.ExecutionsApi.Services.Interfaces;
using System.Collections.Generic;

namespace Rockwell.Monitoring.ExecutionsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecutionsController : ControllerBase
    {
        private readonly IExecutionsService _executionsService;

        public ExecutionsController(IExecutionsService executionsService)
        {
            _executionsService = executionsService;
        }

        [HttpGet]
        public IEnumerable<ExecutionInfo> GetAll()
        {
            return _executionsService.GetAll();
        }

        [HttpGet("job/{jobId}")]
        public IEnumerable<ExecutionInfo> GetByJob(long jobId)
        {
            return _executionsService.GetByJob(jobId);
        }

        [HttpGet("{id}")]
        public ActionResult<ExecutionData> GetById(long id)
        {
            ExecutionData result = _executionsService.GetById(id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }
    }
}
