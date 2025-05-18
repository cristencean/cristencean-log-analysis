using LogAnalysis.Models;
using LogAnalysis.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogAnalysis.Controllers
{
    [ApiController]
    [Route("api/loganalysis")]
    public class LogAnalysisApiController : ControllerBase
    {
        private readonly ILogProcessorService _logProcessorService;

        public LogAnalysisApiController(ILogProcessorService logProcessorService)
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
                return StatusCode(500, new ErrorResponseModel()
                {
                    Message = "An error occurred while processing the log data.",
                    Error = ex.Message
                });
            }
        }
    }
}
