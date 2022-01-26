using System.Text.Json.Serialization;

namespace AzCost.Models
{
    public class AzureResourceConsumption
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("tags")]
        public object Tags { get; set; }

        [JsonPropertyName("properties")]
        public ConsumptionProperties Properties { get; set; }
    }
}