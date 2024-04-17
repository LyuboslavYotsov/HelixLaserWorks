using HelixLaserWorks.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelixLaserWorks.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistic()
        {
            var model = await _statisticService.GetStatisticsAsync();

            return Ok(model);
        }
    }
}
