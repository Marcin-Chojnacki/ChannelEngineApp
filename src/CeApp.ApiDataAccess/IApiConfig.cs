namespace CeApp.ApiDataAccess
{
    public interface IApiConfig
    {
        string BaseUrl { get; }

        string OrdersPath { get; }

        string ProductsPath { get; }

        string ApiKeyHeader { get; }

        string ApiKeyValue { get; }
    }
}
