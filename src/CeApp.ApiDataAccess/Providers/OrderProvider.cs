using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CeApp.ApiDataAccess.DataModels;
using CeApp.DataAccess;
using CeApp.DataObjects.Order;
using Newtonsoft.Json;

namespace CeApp.ApiDataAccess.Providers
{
    public class OrderProvider : BaseProvider, IOrderProvider
    {
        private readonly JsonConverter _orderStatusConverter = new OrderStatusConverter();

        public OrderProvider(IApiConfig apiConfig, HttpClient httpClient) : base(apiConfig, httpClient)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(IDictionary<string, string> filters)
        {
            var mappedFilters = filters.ToDictionary(p => ApiConfig.Orders.GetQueryParam(p.Key), p => p.Value);

            var response = await GetAsync(HttpClient, CreateUrl(mappedFilters, ApiConfig.Orders.BasePath));

            var ordersBundle = JsonConvert.DeserializeObject<OrdersBundle>(response, _orderStatusConverter);

            return ordersBundle.Success ? ordersBundle.Content : throw new ApiException(response);
        }
    }
}
