using Microsoft.AspNetCore.Mvc;

namespace Lin_Tiffany_HW5_V2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}