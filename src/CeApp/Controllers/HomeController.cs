using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using CeApp.ApiDataAccess.Providers;

namespace CeApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Products()
        {
            var productProvider = new ProductProvider(new HttpClient());
            var model = await productProvider.GetProductsAsync(new Dictionary<string, string>());
            return View(model);
        }

    }
}