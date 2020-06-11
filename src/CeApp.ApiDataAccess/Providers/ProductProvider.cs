using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CeApp.ApiDataAccess.DataModels;
using CeApp.DataAccess;
using CeApp.DataObjects.Product;
using Newtonsoft.Json;

namespace CeApp.ApiDataAccess.Providers
{
    public class ProductProvider : BaseProvider, IProductProvider
    {
        public ProductProvider(IApiConfig apiConfig, HttpClient httpClient) : base(apiConfig, httpClient)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(IDictionary<string, string> filters)
        {
            var response = await GetAsync(HttpClient, CreateUrl(filters, ApiConfig.ProductsPath));

            var productsBundle = JsonConvert.DeserializeObject<ProductsBundle>(response);

            return productsBundle.Success ? productsBundle.Content : Enumerable.Empty<Product>();
        }

        public async Task<Product> GetProductAsync(string merchantProductNo)
        {
            var response = await GetAsync(HttpClient, CreateUrl(ApiConfig.ProductsPath, merchantProductNo));

            var productsBundle = JsonConvert.DeserializeObject<ProductItem>(response);

            return productsBundle.Success ? productsBundle.Content : null;
        }
    }
}
