using System.Text.Json.Serialization;

namespace LogAnalysis.Models.Api
{
    public class LogAnalysisApiModel
    {
        [JsonPropertyName("userActivity")]
        public List<UserActivityModel> UserActivity { get; set; } = new List<UserActivityModel>();

        [JsonPropertyName("uniqueUsers")]
        public int UniqueUsers { get; set; } = 0;

        [JsonPropertyName("errors")]
        public ErrorTypeCountModel Errors { get; set; } = new ErrorTypeCountModel();
    }
}
