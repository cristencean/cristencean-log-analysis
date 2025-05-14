using LogAnalysis.Models;
using LogAnalysis.Models.Api;
using System.Text.RegularExpressions;

namespace LogAnalysis.Services;

public class LogProcessorService
{
    private readonly IFileReaderModel _fileReader;
    private readonly string LOG_FILE_PATH = "logs/webrtc_studio.log";
    private readonly string USER_JOIN = "USER_JOIN";
    private readonly string USER_LEAVE = "USER_LEAVE";

    public LogProcessorService(IFileReaderModel fileReader)
    {
        _fileReader = fileReader;
    }

    public LogAnalysisApiModel ProcessLog()
    {
        if (!_fileReader.Exists(LOG_FILE_PATH))
        {
            throw new FileNotFoundException("Log file not found.");
        }

        var logRows = _fileReader.ReadAllLines(LOG_FILE_PATH);
        var logRegex = new Regex(@"\[(.*?)\]\s+(\w+)\s+(\d+)\s+.*");
        var result = new LogAnalysisApiModel();
        var allUserIds = new List<int>();

        foreach (var row in logRows)
        {
            var match = logRegex.Match(row);

            if (!match.Success) 
                continue;

            var timestamp = match.Groups[1].Value;
            var eventAction = match.Groups[2].Value;
            var userId = int.Parse(match.Groups[3].Value);

            allUserIds.Add(userId);

            if (eventAction == USER_JOIN || eventAction == USER_LEAVE)
            {
                result.UserActivity.Add(new UserActivityModel
                {
                    Timestamp = timestamp,
                    Event = eventAction,
                    UserId = userId
                });
            }

            switch(eventAction)
            {
                case "ERROR":
                    result.Errors.ErrorCount++;
                    break;
                case "CRITICAL":
                    result.Errors.CriticalCount++;
                    break;
                case "WARNING":
                    result.Errors.WarningCount++;
                    break;
                default:
                    break;

            }
        }

        result.UniqueUsers = allUserIds.Distinct().Count();

        return result;
    }
}