using System.Collections.Generic;
using System.Threading.Tasks;
using CeApp.Services.Utils;

namespace CeApp.Services.Product
{
    public interface IProductService
    {
        Task<(ResultStatus Status, IEnumerable<DataObjects.Product.Product> Products)> GetProductsAsync(string searchQuery);

        Task<(ResultStatus Status, DataObjects.Product.Product Product)> GetProductAsync(string merchantProductNo);
    }
}
