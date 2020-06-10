using System.Web.Mvc;

namespace CeApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult InternalError()
        {
            return View();
        }
    }
}