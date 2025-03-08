using Microsoft.AspNetCore.Mvc;

namespace SMU.BSIT.BlueHouse.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
