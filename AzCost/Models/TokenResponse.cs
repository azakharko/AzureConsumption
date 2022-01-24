using System.Text.Json.Serialization;

namespace AzCost.Models
{
    internal class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}