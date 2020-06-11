using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CeApp.DataAccess;
using CeApp.Services.Utils;

namespace CeApp.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductProvider _productProvider;

        public ProductService(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        public async Task<(ResultStatus, IEnumerable<DataObjects.Product.Product>)> GetProductsAsync()
        {
            try
            {
                var products = await _productProvider.GetProductsAsync(new Dictionary<string, string>());
                return products == null
                    ? (ResultStatus.UnknownError, null)
                    : (ResultStatus.Success, products.OrderBy(p => p.MerchantProductNo));
            }
            catch (Exception)
            {
                //log ex
                return (ResultStatus.UnknownError, null);
            }
        }

        public async Task<(ResultStatus, DataObjects.Product.Product)> GetProductAsync(string merchantProductNo)
        {
            try
            {
                var product = await _productProvider.GetProductAsync(merchantProductNo);
                return product == null ? (ResultStatus.NotFound, null) : (ResultStatus.Success, product);
            }
            catch (Exception)
            {
                //log ex
                return (ResultStatus.UnknownError, null);
            }
        }
    }
}
