using System.Configuration;

namespace CeApp.ApiDataAccess
{
    public class ApiConfig : ConfigurationSection, IApiConfig
    {
        [ConfigurationProperty("BaseUrl", IsRequired = true)]
        public string BaseUrl => (string)this["BaseUrl"];

        [ConfigurationProperty("OrdersPath", IsRequired = true)]
        public string OrdersPath => (string)this["OrdersPath"];

        [ConfigurationProperty("ProductsPath", IsRequired = true)]
        public string ProductsPath => (string)this["ProductsPath"];

        public string ApiKeyHeader => ConfigurationManager.AppSettings["channelEngineApiKeyHeader"];

        public string ApiKeyValue => ConfigurationManager.AppSettings["channelEngineApiKey"];

    }
}
