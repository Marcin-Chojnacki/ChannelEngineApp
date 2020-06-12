
using Newtonsoft.Json;

namespace CeApp.DataObjects.Product
{
    public class Offer
    {
        public string MerchantProductNo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Stock { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }
    }
}
