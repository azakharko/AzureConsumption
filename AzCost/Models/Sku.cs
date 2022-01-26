using System.Text.Json.Serialization;

namespace AzCost.Models
{
    public class Sku
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tier")]
        public string Tier { get; set; }
    }
}