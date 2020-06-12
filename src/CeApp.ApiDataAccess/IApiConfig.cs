namespace CeApp.ApiDataAccess
{
    public interface IApiConfig
    {
        string ApiKeyHeader { get; }

        string ApiKeyValue { get; }

        string BaseUrl { get; }

        IOrdersConfig Orders { get; }

        IProductsConfig Products { get; }

        IOfferConfig Offer { get; }
    }

    public interface IOrdersConfig
    {
        string BasePath { get; }

        string GetQueryParam(string key);
    }

    public interface IProductsConfig
    {
        string BasePath { get; }

        string GetQueryParam(string key);
    }

    public interface IOfferConfig
    {
        string BasePath { get; }
    }
}