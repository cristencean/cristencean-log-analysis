using System.Text.Json.Serialization;

namespace LogAnalysis.Models
{
    public class ErrorTypeCountModel
    {
        [JsonPropertyName("ERROR")]
        public int ErrorCount { get; set; } = 0;
        [JsonPropertyName("CRITICAL")]
        public int CriticalCount { get; set; } = 0;
        [JsonPropertyName("WARNING")]
        public int WarningCount { get; set; } = 0;
    }
}
