using System;
using System.Linq;
using Autofac;
using CeApp.DataObjects.Order;
using CeApp.DI;
using CeApp.Services.Order;
using CeApp.Services.Product;
using CeApp.Services.Utils;
using ConsoleTables.Core;

namespace CeApp.CUI
{
    internal class Service
    {
        public void GetOrders(string status)
        {
            using (var scope = DiConfig.GetContainer().BeginLifetimeScope())
            {
                var orderService = scope.Resolve<IOrderService>();

                var orderStatus = OrderStatus.Empty;

                if (!string.IsNullOrEmpty(status))
                {
                    orderStatus = OrderStatus.ParseDisplayName(status);
                    if (orderStatus == OrderStatus.Empty)
                        orderStatus = OrderStatus.Parse(status);

                    if (orderStatus == OrderStatus.Empty)
                    {
                        Console.WriteLine("Provided status is incorrect.");
                        return;
                    }
                }

                var orders = orderService.GetOrdersAsync(orderStatus).Result.Orders;

                ConsoleTable.From(orders.Select(o => new {o.Id, o.OrderDate, o.Status})).Write();
            }
        }

        public void GetTopProducts()
        {
            using (var scope = DiConfig.GetContainer().BeginLifetimeScope())
            {
                var productService = scope.Resolve<IProductService>();

                var products = productService.Get5TopProductsAsync().Result.Products;

                ConsoleTable.From(products.Select(p => new
                    {p.Product.Name, p.Product.MerchantProductNo, p.Product.Ean, p.TotalQuantity})).Write();
            }
        }

        public void UpdateStock(string merchantProductNo, int? stock)
        {
            if (string.IsNullOrEmpty(merchantProductNo))
            {
                Console.WriteLine("Cannot update: merchantProductNo is required");
                return;
            }
            if (stock == null)
            {
                Console.WriteLine("Cannot update: stock value is required");
                return;
            }
            if (stock < 0)
            {
                Console.WriteLine("Cannot update: stock cannot be negative number");
                return;
            }

            using (var scope = DiConfig.GetContainer().BeginLifetimeScope())
            {
                var productService = scope.Resolve<IProductService>();

                var isSuccess
                    = productService.UpdateStockAsync(merchantProductNo, stock.Value).Result == ResultStatus.Success;

                Console.WriteLine(isSuccess
                    ? $"Stock for {merchantProductNo} updated to {stock}."
                    : $"Cannot update stock for {merchantProductNo}");
            }
        }
    }
}