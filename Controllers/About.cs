using Microsoft.AspNetCore.Mvc;

namespace JopShop.Controllers
{
    public class About : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
