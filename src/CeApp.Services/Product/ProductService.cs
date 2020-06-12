using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CeApp.DataAccess;
using CeApp.DataObjects.Order;
using CeApp.DataObjects.Product;
using CeApp.Services.Order;
using CeApp.Services.Utils;

namespace CeApp.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductProvider _productProvider;
        private readonly IOrderProvider _orderProvider;
        private readonly IOfferProvider _offerProvider;

        public ProductService(IProductProvider productProvider, IOrderProvider orderProvider, IOfferProvider offerProvider)
        {
            _productProvider = productProvider;
            _orderProvider = orderProvider;
            _offerProvider = offerProvider;
        }

        public async Task<(ResultStatus Status, IEnumerable<DataObjects.Product.Product> Products)> GetProductsAsync(string searchQuery)
        {
            try
            {
                var filters = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(searchQuery))
                    filters.Add(ProductFilters.Search, searchQuery);

                var products = await _productProvider.GetProductsAsync(filters);
                return products == null
                    ? (ResultStatus.UnknownError, null)
                    : (ResultStatus.Success, products.OrderBy(p => p.MerchantProductNo));
            }
            catch (Exception e)
            {
                //log ex
                return (ResultStatus.UnknownError, null);
            }
        }

        public async Task<(ResultStatus Status, IEnumerable<Top5Product> Products)> Get5TopProductsAsync()
        {
            try
            {
                var filters = new Dictionary<string, string> {{OrderFilters.Status, OrderStatus.InProgress.Name}};

                var orders = await _orderProvider.GetOrdersAsync(filters);
                if (orders == null)
                    return (ResultStatus.UnknownError, null);

                var topProductNos = orders.SelectMany(order => order.Lines)
                    .GroupBy(line => line.MerchantProductNo)
                    .Select(group => new
                        {ProductNo = group.Key, TotalQuantity = group.Select(line => line.Quantity).Sum()})
                    .OrderByDescending(x => x.TotalQuantity)
                    .Take(5).ToList();

                if (!topProductNos.Any())
                    return (ResultStatus.Success, Enumerable.Empty<Top5Product>());

                var products = await _productProvider.GetProductsAsync(new Dictionary<string, string>());
                if (products == null)
                    return (ResultStatus.UnknownError, null);

                var result = products.Join(topProductNos, product => product.MerchantProductNo, top => top.ProductNo,
                        (product, top) => new Top5Product {Product = product, TotalQuantity = top.TotalQuantity})
                    .OrderByDescending(product => product.TotalQuantity);

                return (ResultStatus.Success, result);
            }
            catch (Exception e)
            {
                //log ex
                return (ResultStatus.UnknownError, null);
            }
        }

        public async Task<(ResultStatus Status, DataObjects.Product.Product Product)> GetProductAsync(string merchantProductNo)
        {
            try
            {
                var product = await _productProvider.GetProductAsync(merchantProductNo);
                return product == null ? (ResultStatus.NotFound, null) : (ResultStatus.Success, product);
            }
            catch (Exception e)
            {
                //log ex
                return (ResultStatus.UnknownError, null);
            }
        }

        public async Task<ResultStatus> UpdateStockAsync(string merchantProductNo, int stock)
        {
            try
            {
                var offer = new Offer
                {
                    MerchantProductNo = merchantProductNo,
                    Stock = stock
                };
                var isSuccess = await _offerProvider.UpdateOfferAsync(offer);

                return isSuccess ? ResultStatus.Success : ResultStatus.UnknownError;
            }
            catch (Exception e)
            {
                //log ex
                return ResultStatus.UnknownError;
            }
        }
    }
}
