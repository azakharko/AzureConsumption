using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzCost.Models
{
    public class ResourceGroupInfo
    {
        public ResourceGroupInfo(string rgName, string subscriptionId)
        {
            RgName = rgName;
            SubscriptionId = subscriptionId;

            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Id = $"{SubscriptionId}{RgName}_{Timestamp}";
        }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; private set; }

        [JsonPropertyName("id")]
        public string Id { get; private set; }

        [JsonPropertyName("rgName")]
        public string RgName { get; private set; }

        [JsonPropertyName("subscriptionId")]
        public string SubscriptionId { get; private set; }

        [JsonPropertyName("resources")]
        public List<AzureResource> Resources { get; } = new List<AzureResource>();

        [JsonPropertyName("consumption")]
        public List<AzureResourceConsumption> Consumption { get; set; }
    }
}