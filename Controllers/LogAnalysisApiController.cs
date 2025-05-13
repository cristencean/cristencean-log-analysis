using LogAnalysis.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogAnalysis.Controllers
{
    [ApiController]
    [Route("api/loganalysis")]
    public class LogAnalysisApiController : ControllerBase
    {
        private readonly LogProcessorService _logProcessorService;

        public LogAnalysisApiController(LogProcessorService logProcessorService)
        {
            _logProcessorService = logProcessorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _logProcessorService.ProcessLog();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while processing the log data.",
                    error = ex.Message
                });
            }
        }
    }
}
