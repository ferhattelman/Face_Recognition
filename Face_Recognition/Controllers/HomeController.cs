using Face_Recognition.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

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
        [HttpGet]
        public IActionResult FPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FPassword(IFormCollection datas)
        {
            string email = datas["email"].ToString();
            FCheck check = new FCheck();
            try
            {
                string godMail = "ferhattelman@outlook.com";
                string godPass = "phL26VRydF";
                string pass = check.Control(email);
                if(pass !=null)
                {
                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = 587;
                    smtp.Host = "smtp.outlook.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(godMail, godPass);
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(godMail, "Ferhat Telman");
                    mail.To.Add(email);
                    mail.Subject = "Password";
                    mail.IsBodyHtml = true;
                    mail.Body = pass;
                    smtp.Send(mail);

                    ViewBag.message = "Email sent successfully!";
                }
                else
                {
                    ViewBag.message = "There is a conflict in the system, please try again!";
                }
            }
            catch (Exception)
            {

                throw;
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