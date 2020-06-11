using System.Collections.Generic;
using System.Threading.Tasks;
using CeApp.DataObjects.Order;
using CeApp.Services.Utils;

namespace CeApp.Services.Order
{
    public interface IOrderService
    {
        Task<(ResultStatus Status, IEnumerable<DataObjects.Order.Order> Orders)> GetOrdersAsync(OrderStatus orderStatus);

//        Task<(ResultStatus Status, DataObjects.Product.Product Product)> GetProductAsync(string merchantProductNo);
    }
}
