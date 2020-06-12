using System.Threading.Tasks;
using System.Web.Mvc;
using CeApp.Services.Product;
using CeApp.Services.Utils;

namespace CeApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ActionResult> Index(string search = null)
        {
            var (status, products) = await _productService.GetProductsAsync(search);
            if(status == ResultStatus.Success)
                return View(products);

            return RedirectToAction("InternalError", "Error");
        }

        public async Task<ActionResult> GetTop5()
        {
            var (status, products) = await _productService.Get5TopProductsAsync();
            if (status == ResultStatus.Success)
                return View(products);

            return RedirectToAction("InternalError", "Error");
        }

        public async Task<ActionResult> Get(string merchantProductNo)
        {

            if (string.IsNullOrEmpty(merchantProductNo))
                return RedirectToAction("NotFound", "Error");

            var (status, product) = await _productService.GetProductAsync(merchantProductNo);
            if (status == ResultStatus.Success)
            {
                LoadMessage();
                return View(product);
            }

            if (status == ResultStatus.NotFound)
                return RedirectToAction("NotFound", "Error");

            return RedirectToAction("InternalError", "Error");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStock(string merchantProductNo, int stock)
        {
            if (stock < 0)
            {
                AddErrorMessage("Cannot update: Stock cannot be negative number.");
                return RedirectToAction("Get", new {merchantProductNo});
            }

            var status = await _productService.UpdateStockAsync(merchantProductNo, stock);

            if (status == ResultStatus.Success)
                AddSuccessMessage("Product successfully updated.");
            else
                AddErrorMessage("Cannot update: Internal error.");

            return RedirectToAction("Get", new { merchantProductNo });
        }

        private void AddSuccessMessage(string message)
        {
            TempData["message"] = message;
            TempData["messageType"] = "success";
        }

        private void AddErrorMessage(string message)
        {
            TempData["message"] = message;
            TempData["messageType"] = "danger";
        }

        private void LoadMessage()
        {
            ViewBag.Message = TempData["message"];
            ViewBag.MessageType = TempData["messageType"];
        }
    }
}
