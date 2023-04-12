using Microsoft.AspNetCore.Mvc;

namespace Face_Recognition.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Record()
        {
            return View();
        }
        public IActionResult Board()
        {
            return View();
        }
    }
}
