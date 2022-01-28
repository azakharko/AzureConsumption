using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AzCost.Models;
using AzCost.Repositories;
using Microsoft.Extensions.Configuration;

namespace AzCost
{
    internal class Worker
    {
        private readonly HttpClient _client = new()
        {
            BaseAddress = new Uri("https://management.azure.com")
        };

        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;

        public Worker(IConfiguration configuration, IRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task GetResourceInfoAsync()
        {
            var subscriptions = _configuration.GetSection("Subscriptions").Get<IEnumerable<SettingsSubscription>>();

            try
            {
                foreach (var subscription in subscriptions)
                {
                    await SetAuthTokenForTenantAsync(subscription.TenantId);

                    foreach (var rgName in subscription.ResourceGroups)
                    {
                        var rgInfo = await GetResourcesAsync(subscription.Id, rgName);
                        rgInfo.Consumption = await GetResourceConsumptionAsync(subscription.Id, rgName);

                        PrintResources(rgInfo);

                        await _repository.SaveRgInfoAsync(rgInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("================================================");
                Console.WriteLine(ex.Message);
                Console.WriteLine("================================================");
            }
        }

        private async Task<List<AzureResourceConsumption>> GetResourceConsumptionAsync(string subscriptionId, string rgName)
        {
            var consumption = new List<AzureResourceConsumption>();

            var uri = $"subscriptions/{subscriptionId}/providers/Microsoft.Consumption/usagedetails?$filter=properties/resourceGroup eq '{rgName}'&api-version=2021-10-01";

            do
            {
                var response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var resources = await JsonSerializer.DeserializeAsync<AzureApiResponse<AzureResourceConsumption>>(await response.Content.ReadAsStreamAsync());
                consumption.AddRange(resources.Value);

                uri = resources.NextLink;

            } while (uri != null);

            return consumption;
        }

        private static void PrintResources(ResourceGroupInfo rgInfo)
        {
            Console.WriteLine($"Processing resource group {rgInfo.RgName}");
            foreach (var resource in rgInfo.Resources)
            {
                Console.WriteLine($"Resource Id {resource.Id}, Name {resource.Name}");
                Console.WriteLine($"Type {resource.Type}, Sku {resource.Sku?.Name}, Location {resource.Location}");
                Console.WriteLine($"Created at {resource.CreatedTime}, changed at {resource.ChangedTime}");
                if (resource.Tags?.Count > 0)
                {
                    Console.Write($"Tags ");
                    foreach (var (key, value) in resource.Tags)
                    {
                        Console.Write($"{key}: {value},");
                    }

                    Console.WriteLine();
                }

                var consumptionData = rgInfo.Consumption.Where(c => c.Properties?.ResourceId == resource.Id);

                foreach (var dailyConsumption in consumptionData)
                {
                    Console.WriteLine($"Consumed {dailyConsumption.Properties.EffectivePrice}{dailyConsumption.Properties.BillingCurrency} for {dailyConsumption.Properties.Date}");
                }

                Console.WriteLine();

            }
        }

        private async Task<ResourceGroupInfo> GetResourcesAsync(string subscriptionId, string rgName)
        {
            var rgInfo = new ResourceGroupInfo(rgName, subscriptionId);

            var uri = $"subscriptions/{subscriptionId}/resourceGroups/{rgName}/resources?$expand=createdTime,changedTime&api-version=2021-04-01";

            do
            {
                var response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var resources = await JsonSerializer.DeserializeAsync<AzureApiResponse<AzureResource>>(await response.Content.ReadAsStreamAsync());
                rgInfo.Resources.AddRange(resources.Value);

                uri = resources.NextLink;

            } while (uri != null);

            return rgInfo;
        }

        private async Task SetAuthTokenForTenantAsync(string tenantId)
        {
            var clientSecret = _configuration.GetValue<string>("OpenId:ClientSecret");
            var clientId = _configuration.GetValue<string>("OpenId:ClientId");

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("client_id", clientId));
            nvc.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            nvc.Add(new KeyValuePair<string, string>("scope", "https://management.core.windows.net/.default"));
            nvc.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token") { Content = new FormUrlEncodedContent(nvc) };

            var responseMessage = await _client.SendAsync(request);

            responseMessage.EnsureSuccessStatusCode();

            var tokenResponse = await responseMessage.Content.ReadFromJsonAsync<TokenResponse>();

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenResponse.AccessToken}");
        }
    }
}
