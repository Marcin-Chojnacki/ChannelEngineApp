using System.Collections.Generic;
using System.Linq;

namespace CeApp.DataObjects.Order
{
    public class OrderStatus
    {
        public string Name { get; private set; }

        public string DisplayName { get; private set; }

        public static readonly OrderStatus InProgress = new OrderStatus
        {
            DisplayName = "InProgress",
            Name = "IN_PROGRESS"
        };

        public static readonly OrderStatus Shipped = new OrderStatus
        {
            DisplayName = "Shipped",
            Name = "SHIPPED"
        };

        public static readonly OrderStatus Empty = new OrderStatus
        {
            DisplayName = "",
            Name = ""
        };

        public static IEnumerable<OrderStatus> GetAll() => All;

        public static OrderStatus Parse(string name)
        {
            return All.FirstOrDefault(s => s.Name.Equals(name)) ?? Empty;
        }


        private static readonly List<OrderStatus> All = new List<OrderStatus>
        {
            InProgress, Shipped
        };
    }
}