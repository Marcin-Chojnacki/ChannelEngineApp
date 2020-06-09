using System.Collections.Generic;
using System.Threading.Tasks;
using CeApp.DataObjects.Order;

namespace CeApp.DataAccess
{
    public interface IOrderProvider
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
    }
}
