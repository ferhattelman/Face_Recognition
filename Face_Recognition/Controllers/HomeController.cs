using Face_Recognition.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Face_Recognition.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection datas)
        {
            string username = datas["username"].ToString();
            string password = datas["password"].ToString();
            Check check = new Check();
            if (check.Control(username, password) == true)
            {
                return View("Dashboard", "Dashboard");

            }
            else
            {
                //Hatalı uyarısı 
                Response.Redirect("Hatali");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}