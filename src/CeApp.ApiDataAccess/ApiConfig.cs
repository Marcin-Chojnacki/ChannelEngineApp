using System;
using System.Collections.Generic;
using System.Configuration;

namespace CeApp.ApiDataAccess
{
    public class ApiConfig : IApiConfig
    {
        public ApiConfig()
        {
            var apiConfigSection = ConfigurationManager.GetSection("channelEngineApiConfig") as ApiConfigSection;

            Orders = new OrdersConfig(apiConfigSection);
            Products = new ProductsConfig();
        }

        public string ApiKeyHeader => ConfigurationManager.AppSettings["channelEngineApiKeyHeader"];

        public string ApiKeyValue => ConfigurationManager.AppSettings["channelEngineApiKey"];

        public string BaseUrl => ConfigurationManager.AppSettings["channelEngineApiBaseUrl"];

        public IOrdersConfig Orders { get; }

        public IProductsConfig Products { get; }
    }

    internal class OrdersConfig : IOrdersConfig
    {
        private readonly ApiConfigSection _configSection;
        private static readonly IDictionary<string, string> FilterMapping = new Dictionary<string, string>
        {
            {"orderStatusQuery", "statuses"}
        };

        public OrdersConfig(ApiConfigSection configSection)
        {
            _configSection = configSection;
        }

        public string BasePath => "orders";

        public string GetQueryParam(string key)
        {
            if (FilterMapping.TryGetValue(_configSection.OrderFiltersMappingProperty.GetValue(key), out var param))
                return param;
            throw new ArgumentOutOfRangeException(nameof(key), $"Not found following element: {key}");
        }
    }

    internal class ProductsConfig : IProductsConfig
    {
        public string BasePath => "products";
    }

    public class ApiConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("orderFiltersMapping", IsRequired = true)]
        public BaseConfigCollection OrderFiltersMappingProperty => (BaseConfigCollection) this["orderFiltersMapping"];
    }

}