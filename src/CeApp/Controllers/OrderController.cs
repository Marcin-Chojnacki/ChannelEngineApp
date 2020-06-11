using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using CeApp.DataObjects.Order;
using CeApp.Services.Order;
using CeApp.Services.Utils;

namespace CeApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ActionResult> Index(string orderStatusName = null)
        {
            var orderStatus = OrderStatus.Parse(orderStatusName);

            var (status, products) = await _orderService.GetOrdersAsync(orderStatus);
            if(status == ResultStatus.Success)
                return View(products);

            return RedirectToAction("InternalError", "Error");
        }

    }
}