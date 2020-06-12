using System.Net.Http;
using System.Threading.Tasks;
using CeApp.ApiDataAccess.DataModels;
using CeApp.DataAccess;
using CeApp.DataObjects.Product;
using Newtonsoft.Json;

namespace CeApp.ApiDataAccess.Providers
{
    public class OfferProvider : BaseProvider, IOfferProvider
    {
        public OfferProvider(IApiConfig apiConfig, HttpClient httpClient) : base(apiConfig, httpClient)
        {
        }

       public async Task<bool> UpdateOfferAsync(Offer offer)
        {
            var json = JsonConvert.SerializeObject(new[] {offer});

            var response = await PutAsync(HttpClient, CreateUrl(ApiConfig.Offer.BasePath), json);

            var updateResult = JsonConvert.DeserializeObject<UpdateOfferResult>(response);

            return updateResult.Success;
        }
    }
}
