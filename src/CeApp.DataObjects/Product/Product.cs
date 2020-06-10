namespace CeApp.DataObjects.Product
{
    public class Product
    {
        public string MerchantProductNo { get; set; }

        public string Ean { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
