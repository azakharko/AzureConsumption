using System.Collections.Generic;

namespace AzCost.Models
{
    public class ResourceGroupInfo
    {
        public string RgName { get; set; }
        public string SubscriptionId { get; set; }
        public List<AzureResource> Resources { get; set; }
        public List<AzureResourceConsumption> Consumption { get; set; }
    }
}