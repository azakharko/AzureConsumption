using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AzCost.Infrastructure;
using AzCost.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace AzCost.Repositories
{
    class CosmosDbRepository : IRepository
    {
        private readonly IConfiguration _configuration;
        private Container _container;

        private const string ContainerId = "ResourceGroups";
        private const string DatabaseId = "AzureInfo";

        public CosmosDbRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SaveRgInfoAsync(ResourceGroupInfo rgInfo)
        {
            await EnsureContainerCreatedAsync();

            await _container.CreateItemAsync<ResourceGroupInfo>(rgInfo, new PartitionKey(rgInfo.RgName));
        }

        public async Task EnsureContainerCreatedAsync()
        {
            if (_container != default)
                return;

            var endpointUri = _configuration.GetValue<string>("CosmosDb:Endpoint");
            var primaryKey = _configuration.GetValue<string>("CosmosDb:Key");

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var cosmosClient = new CosmosClient(endpointUri, primaryKey,
                new CosmosClientOptions
                {
                    ApplicationName = "AzureCostExplorer",
                    Serializer = new CosmosSystemTextJsonSerializer(jsonSerializerOptions)
                });

            var databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
            var containerResponse = await databaseResponse.Database.CreateContainerIfNotExistsAsync(ContainerId, "/rgName");

            _container = containerResponse.Container;
        }
    }
}