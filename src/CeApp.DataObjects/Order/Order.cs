using System;
using System.Collections.Generic;

namespace CeApp.DataObjects.Order
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<Line> Lines { get; set; }
    }
}
