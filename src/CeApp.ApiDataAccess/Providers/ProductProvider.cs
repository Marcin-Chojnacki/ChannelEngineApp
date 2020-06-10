using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiDataAccess.DataModels;
using CeApp.DataAccess;
using CeApp.DataObjects.Product;

namespace CeApp.ApiDataAccess.Providers
{
    public class ProductProvider : BaseProvider, IProductProvider
    {
        public ProductProvider(IApiConfig apiConfig, HttpClient httpClient) : base(apiConfig, httpClient)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(IDictionary<string, string> filters)
        {
            var productsBundle = 
                await GetAsync<ProductsBundle>(HttpClient, CreateUrl(filters, ApiConfig.ProductsPath));

            return productsBundle.Success ? productsBundle.Content : Enumerable.Empty<Product>();
        }

        public async Task<Product> GetProductAsync(string merchantProductNo)
        {
            var productsBundle =
                await GetAsync<ProductItem>(HttpClient, CreateUrl(ApiConfig.ProductsPath, merchantProductNo));

            return productsBundle.Success ? productsBundle.Content : null;
        }
    }
}
