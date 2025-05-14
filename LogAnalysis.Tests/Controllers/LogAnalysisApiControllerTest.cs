using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LogAnalysis.Controllers;
using LogAnalysis.Models.Api;
using LogAnalysis.Models;

namespace LogAnalysis.Tests.Controllers
{
    public class LogAnalysisApiControllerTest
    {
        [Fact]
        public void Get_ReturnsOk_WhenProcessLogReturnsData()
        {
            var mockService = new Mock<ILogProcessorService>();
            var mockData = new LogAnalysisApiModel
            {
                UniqueUsers = 11,
                UserActivity = [],
                Errors = new ErrorTypeCountModel { ErrorCount = 2, WarningCount = 5, CriticalCount = 7 }
            };

            mockService.Setup(s => s.ProcessLog()).Returns(mockData);
            var controllerMock = new LogAnalysisApiController(mockService.Object);
            var result = controllerMock.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<LogAnalysisApiModel>(okResult.Value);
            Assert.Equal(11, data.UniqueUsers);
            Assert.Equal(2, data.Errors.ErrorCount);
            Assert.Equal(5, data.Errors.WarningCount);
            Assert.Equal(7, data.Errors.CriticalCount);
        }

        [Fact]
        public void Get_Returns500_WhenProcessLogNotReturnData()
        {
            var mockService = new Mock<ILogProcessorService>();
            mockService.Setup(s => s.ProcessLog()).Throws(new Exception("Could not read the file!"));

            var controller = new LogAnalysisApiController(mockService.Object);
            var result = controller.Get();

            var errorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, errorResult.StatusCode);

            var errorResponse = Assert.IsAssignableFrom<ErrorResponseModel>(errorResult.Value);
            Assert.Equal("An error occurred while processing the log data.", errorResponse.Message);
            Assert.Equal("Could not read the file!", errorResponse.Error);
        }
    }
}