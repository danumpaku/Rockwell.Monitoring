using Microsoft.AspNetCore.Mvc;
using Rockwell.Monitoring.Scraper.Models.Requests;
using Rockwell.Monitoring.Scraper.Services.Interfaces;
using System.Threading.Tasks;

namespace Rockwell.Monitoring.Scraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScrapeController : ControllerBase
    {
        private readonly IScraperService _scraperService;

        public ScrapeController(IScraperService scraperService)
        {
            _scraperService = scraperService;
        }

        [HttpPost]
        public async Task<ActionResult> ProcessScrape(ScrapeRequest request)
        {
            await _scraperService.ExecuteScrape(request);
            return Ok();
        }
    }
}
