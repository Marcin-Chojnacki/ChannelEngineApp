using System;

namespace CeApp.DataObjects.Order
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
