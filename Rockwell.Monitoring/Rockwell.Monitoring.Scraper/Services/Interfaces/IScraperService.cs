using Rockwell.Monitoring.Scraper.Models.Requests;
using System.Threading.Tasks;

namespace Rockwell.Monitoring.Scraper.Services.Interfaces
{
    public interface IScraperService
    {
        Task ExecuteScrape(ScrapeRequest request);
    }
}
