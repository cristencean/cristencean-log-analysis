using System.Text.Json.Serialization;

namespace LogAnalysis.Models
{
    public class UserActivityModel
    {

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;
    }
}
