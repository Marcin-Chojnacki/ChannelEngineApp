namespace CeApp.DataObjects.Product
{
    public class Product
    {
        public string MerchantProductNo { get; set; }
         
        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string Ean { get; set; }

        public int? Stock { get; set; }

        public decimal? Price { get; set; }

        public decimal? ShippingCost { get; set; }
    }
}
