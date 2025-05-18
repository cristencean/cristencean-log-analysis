using Xunit;
using Moq;
using LogAnalysis.Services;
using LogAnalysis.Models;

namespace LogAnalysis.Tests.Services
{
    public class LogProcessorServiceTest
    {
        [Fact]
        public void ProcessLog_ShouldReturnCorrectData()
        {
            var mockReader = new Mock<IFileReaderModel>();

            var mockData = new string[]
            {
                "[2023-10-27 10:07:11] CRITICAL 176 stream_1058 Stream failure, attempting restart [Error Code:500, Reason:Network Timeout]",
                "[2023-10-27 10:07:59] STREAM_START 140 stream_1014 User 140 started stream stream_1014 [Codec:H264, Bitrate:1500kbps]",
                "[2023-10-27 10:08:43] STREAM_START 129 stream_1061 User 129 started stream stream_1061 [Codec:VP8, Bitrate:1500kbps]",
                "[2023-10-27 10:08:50] USER_LEAVE 150 stream_1099 User 150 left the session",
                "[2023-10-27 10:09:38] DEBUG 152 stream_1041 Audio input device not detected. [Device:Microphone]",
                "[2023-10-27 10:10:00] STREAM_START 197 stream_1037 User 197 started stream stream_1037 [Codec:VP8, Bitrate:1500kbps]",
                "[2023-10-27 10:10:23] WARNING 122 stream_1052 Audio input device not detected. [Device:Microphone]",
                "[2023-10-27 10:10:29] STREAM_RESTART 160 stream_1019 Stream restarted.",
                "[2023-10-27 10:11:28] CRITICAL 196 stream_1036 Stream failure, attempting restart [Error Code:501, Reason:Internal Server Error]",
                "[2023-10-27 10:12:26] WARNING 112 stream_1054 High CPU usage detected [CPU:90%]",
                "[2023-10-27 10:13:17] WARNING 163 stream_1082 User audio connected.",
                "[2023-10-27 10:13:46] USER_LEAVE 114 stream_1040 User 114 left the session",
                "[2023-10-27 10:14:02] ERROR 140 stream_1006 Stream error [Error Code:500, Reason:Network Timeout]",
                "[2023-10-27 10:14:32] INFO 104 stream_1032 High CPU usage detected [CPU:90%] [2023-10-27 10:07:11] CRITICAL 176 stream_1058 Stream failure, attempting restart [Error Code:500, Reason:Network Timeout]",
                "[2023-10-27 10:07:59] STREAM_START 140 stream_1014 User 140 started stream stream_1014 [Codec:H264, Bitrate:1500kbps]",
                "[2023-10-27 10:08:43] STREAM_START 129 stream_1061 User 129 started stream stream_1061 [Codec:VP8, Bitrate:1500kbps]",
                "[2023-10-27 10:08:50] USER_LEAVE 150 stream_1099 User 150 left the session",
                "[2023-10-27 10:09:38] DEBUG 152 stream_1041 Audio input device not detected. [Device:Microphone]",
                "[2023-10-27 10:10:00] STREAM_START 197 stream_1037 User 197 started stream stream_1037 [Codec:VP8, Bitrate:1500kbps]",
                "[2023-10-27 10:10:23] WARNING 122 stream_1052 Audio input device not detected. [Device:Microphone]",
                "[2023-10-27 10:10:29] STREAM_RESTART 160 stream_1019 Stream restarted.",
                "[2023-10-27 10:11:28] CRITICAL 196 stream_1036 Stream failure, attempting restart [Error Code:501, Reason:Internal Server Error]",
                "[2023-10-27 10:12:26] WARNING 112 stream_1054 High CPU usage detected [CPU:90%]",
                "[2023-10-27 10:13:17] WARNING 163 stream_1082 User audio connected.",
                "[2023-10-27 10:13:46] USER_LEAVE 114 stream_1040 User 114 left the session",
                "[2023-10-27 10:14:02] ERROR 140 stream_1006 Stream error [Error Code:500, Reason:Network Timeout]",
                "[2023-10-27 10:14:32] INFO 104 stream_1032 High CPU usage detected [CPU:90%]",
                "[2023-10-27 10:19:13] USER_JOIN 102 stream_1036 User 102 joined the session"
            };

            mockReader.Setup(file => file.Exists(It.IsAny<string>())).Returns(true);
            mockReader.Setup(file => file.ReadAllLines(It.IsAny<string>())).Returns(mockData);

            var logProcessorService = new LogProcessorService(mockReader.Object);

            var result = logProcessorService.ProcessLog();

            Assert.NotNull(result);
            Assert.Equal(14, result.UniqueUsers);
            Assert.Equal(5, result.UserActivity.Count);
            Assert.Equal(2, result.Errors.ErrorCount);
            Assert.Equal(6, result.Errors.WarningCount);
            Assert.Equal(3, result.Errors.CriticalCount);
        }

        [Fact]
        public void ProcessLog_ShouldThrowNotFoundIfFileDoesNotExist()
        {
            var mockReader = new Mock<IFileReaderModel>();
            mockReader.Setup(file => file.Exists(It.IsAny<string>())).Returns(false);

            var service = new LogProcessorService(mockReader.Object);

            Assert.Throws<FileNotFoundException>(() => service.ProcessLog());
        }
    }
}