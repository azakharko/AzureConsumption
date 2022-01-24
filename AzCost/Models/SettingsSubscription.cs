using System.Collections.Generic;

namespace AzCost.Models
{
    public class SettingsSubscription
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public List<string> ResourceGroups { get; set; }
    }
}