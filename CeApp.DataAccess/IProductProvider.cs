using System.Collections.Generic;
using System.Threading.Tasks;
using CeApp.DataObjects.Product;

namespace CeApp.DataAccess
{
    public interface IProductProvider
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
