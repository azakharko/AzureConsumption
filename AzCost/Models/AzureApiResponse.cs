using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzCost.Models
{
    public class AzureApiResponse<T>
    {
        [JsonPropertyName("value")]
        public List<T> Value { get; set; }

        [JsonPropertyName("nextLink")]
        public string NextLink { get; set; }
    }
}