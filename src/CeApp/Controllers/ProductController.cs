using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using CeApp.ApiDataAccess.Providers;
using CeApp.Services.Product;
using CeApp.Services.Utils;

namespace CeApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

//        public ProductController(IProductService productService)
        public ProductController()
        {
            _productService = new ProductService(new ProductProvider(new HttpClient()));
        }

        public async Task<ActionResult> Index()
        {
            var (status, products) = await _productService.GetProductsAsync();
            if(status == ResultStatus.Success)
                return View(products);

            return RedirectToAction("InternalError", "Error");
        }

        public async Task<ActionResult> Get(string merchantProductNo)
        {
            var (status, product) = await _productService.GetProductAsync(merchantProductNo);
            if(status == ResultStatus.Success)
                return View(product);

            if (status == ResultStatus.NotFound)
                return RedirectToAction("NotFound", "Error");

            return RedirectToAction("InternalError", "Error");
        }

    }
}