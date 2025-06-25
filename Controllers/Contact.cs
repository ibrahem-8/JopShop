using Microsoft.AspNetCore.Mvc;

namespace JopShop.Controllers
{
    public class Contact : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
