using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzCost.Models
{
    public class AzureResource
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("sku")]
        public Sku Sku { get; set; }

        [JsonPropertyName("managedBy")]
        public string ManagedBy { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("createdTime")]
        public DateTime CreatedTime { get; set; }

        [JsonPropertyName("changedTime")]
        public DateTime ChangedTime { get; set; }

        [JsonPropertyName("tags")]
        public Dictionary<string, string> Tags { get; set; }
    }
}