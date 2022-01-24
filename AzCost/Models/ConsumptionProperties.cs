using System;
using System.Text.Json.Serialization;

namespace AzCost.Models
{
    public class ConsumptionProperties
    {
        [JsonPropertyName("billingAccountId")]
        public string BillingAccountId { get; set; }

        [JsonPropertyName("billingAccountName")]
        public string BillingAccountName { get; set; }

        [JsonPropertyName("billingPeriodStartDate")]
        public DateTime BillingPeriodStartDate { get; set; }

        [JsonPropertyName("billingPeriodEndDate")]
        public DateTime BillingPeriodEndDate { get; set; }

        [JsonPropertyName("billingProfileId")]
        public string BillingProfileId { get; set; }

        [JsonPropertyName("billingProfileName")]
        public string BillingProfileName { get; set; }

        [JsonPropertyName("accountOwnerId")]
        public string AccountOwnerId { get; set; }

        [JsonPropertyName("accountName")]
        public string AccountName { get; set; }

        [JsonPropertyName("subscriptionId")]
        public string SubscriptionId { get; set; }

        [JsonPropertyName("subscriptionName")]
        public string SubscriptionName { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("product")]
        public string Product { get; set; }

        [JsonPropertyName("partNumber")]
        public string PartNumber { get; set; }

        [JsonPropertyName("meterId")]
        public string MeterId { get; set; }

        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("effectivePrice")]
        public double EffectivePrice { get; set; }

        [JsonPropertyName("cost")]
        public double Cost { get; set; }

        [JsonPropertyName("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonPropertyName("billingCurrency")]
        public string BillingCurrency { get; set; }

        [JsonPropertyName("resourceLocation")]
        public string ResourceLocation { get; set; }

        [JsonPropertyName("consumedService")]
        public string ConsumedService { get; set; }

        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; }

        [JsonPropertyName("resourceName")]
        public string ResourceName { get; set; }

        [JsonPropertyName("invoiceSection")]
        public string InvoiceSection { get; set; }

        [JsonPropertyName("costCenter")]
        public string CostCenter { get; set; }

        [JsonPropertyName("resourceGroup")]
        public string ResourceGroup { get; set; }

        [JsonPropertyName("offerId")]
        public string OfferId { get; set; }

        [JsonPropertyName("isAzureCreditEligible")]
        public bool IsAzureCreditEligible { get; set; }

        [JsonPropertyName("publisherType")]
        public string PublisherType { get; set; }

        [JsonPropertyName("chargeType")]
        public string ChargeType { get; set; }

        [JsonPropertyName("frequency")]
        public string Frequency { get; set; }

        [JsonPropertyName("payGPrice")]
        public int PayGPrice { get; set; }

        [JsonPropertyName("pricingModel")]
        public string PricingModel { get; set; }

        [JsonPropertyName("meterDetails")]
        public object MeterDetails { get; set; }

        [JsonPropertyName("serviceInfo2")]
        public string ServiceInfo2 { get; set; }
    }
}