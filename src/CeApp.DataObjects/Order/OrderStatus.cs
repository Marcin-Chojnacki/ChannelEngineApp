using System;
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

        public static readonly OrderStatus InBackOrder = new OrderStatus
        {
            DisplayName = "InBackOrder",
            Name = "IN_BACKORDER"
        };

        public static readonly OrderStatus Manco = new OrderStatus
        {
            DisplayName = "Manco",
            Name = "MANCO"
        };

        public static readonly OrderStatus InCombi = new OrderStatus
        {
            DisplayName = "InCombi",
            Name = "IN_COMBI"
        };

        public static readonly OrderStatus Closed = new OrderStatus
        {
            DisplayName = "Closed",
            Name = "CLOSED"
        };

        public static readonly OrderStatus New = new OrderStatus
        {
            DisplayName = "New",
            Name = "NEW"
        };

        public static readonly OrderStatus Returned = new OrderStatus
        {
            DisplayName = "Returned",
            Name = "RETURNED"
        };

        public static readonly OrderStatus RequiresCorrection = new OrderStatus
        {
            DisplayName = "RequiresCorrection",
            Name = "REQUIRES_CORRECTION"

        };

        public static readonly OrderStatus Empty = new OrderStatus
        {
            DisplayName = "",
            Name = ""
        };

        public static IEnumerable<OrderStatus> GetAll() => All;

        public static OrderStatus Parse(string name)
        {
            return All.FirstOrDefault(s => s.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? Empty;
        }

        public static OrderStatus ParseDisplayName(string name)
        {
            return All.FirstOrDefault(s => s.DisplayName.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? Empty;
        }

        public override string ToString() => DisplayName;
        
        private static readonly List<OrderStatus> All = new List<OrderStatus>
        {
            InProgress, Shipped, InBackOrder, Manco, InCombi, Closed, Returned, RequiresCorrection
        };
    }
}