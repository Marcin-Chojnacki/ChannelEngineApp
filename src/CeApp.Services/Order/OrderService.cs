using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CeApp.DataAccess;
using CeApp.DataObjects.Order;
using CeApp.Services.Utils;

namespace CeApp.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderProvider _orderProvider;

        public OrderService(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }

        public async Task<(ResultStatus Status, IEnumerable<DataObjects.Order.Order> Orders)> GetOrdersAsync(
            OrderStatus orderStatus)
        {
            try
            {
                var filters = new Dictionary<string, string>();
                if (orderStatus != OrderStatus.Empty)
                    filters.Add(OrderFilters.Status, orderStatus.Name);

                var orders = await _orderProvider.GetOrdersAsync(filters);
                return orders == null
                    ? (ResultStatus.UnknownError, null)
                    : (ResultStatus.Success, orders.OrderBy(o => o.Id));
            }
            catch (Exception)
            {
                //log ex
                return (ResultStatus.UnknownError, null);
            }
        }

    }
}
