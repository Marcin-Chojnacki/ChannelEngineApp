using System.Threading.Tasks;
using CeApp.DataObjects.Product;

namespace CeApp.DataAccess
{
    public interface IOfferProvider
    {
        Task UpdateOfferAsync(Offer offer);
    }
}
