using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;

namespace CeApp.ApiDataAccess.Providers
{
    public abstract class BaseProvider
    {
        protected readonly IApiConfig ApiConfig;
        protected readonly HttpClient HttpClient;

        protected BaseProvider(IApiConfig apiConfig, HttpClient httpClient)
        {
            ApiConfig = apiConfig;
            HttpClient = httpClient;
            HttpClient.DefaultRequestHeaders.Add(ApiConfig.ApiKeyHeader, ApiConfig.ApiKeyValue);
        }

        protected async Task<string> GetAsync(HttpClient httpClient, string url)
        {
            using (var response = await httpClient.GetAsync(url))
            {
                return await response.Content.ReadAsStringAsync();

//                return JsonConvert.DeserializeObject<T>(responseText);
            }
        }

        protected string CreateUrl(params string[] pathElements)
        {
            return CreateUrl(new Dictionary<string, string>(), pathElements);
        }

        protected string CreateUrl(IDictionary<string, string> queryParameters, params string[] pathElements)
        {
            return ApiConfig.BaseUrl
                .AppendPathSegments(pathElements.ToArray<object>())
                .SetQueryParams(queryParameters)
                .ToString();
        }
    }
}
