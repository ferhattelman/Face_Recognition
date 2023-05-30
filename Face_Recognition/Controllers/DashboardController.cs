using Face_Recognition.EntityStore;
using Face_Recognition.Models;
using Microsoft.AspNetCore.Mvc;

namespace Face_Recognition.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly DatabaseContext _databaseContext;
        public DashboardController(IWebHostEnvironment environment, DatabaseContext databaseContext)
        {
            _environment = environment; // Çevre kontrolü
            _databaseContext = databaseContext;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
       
        public IActionResult Board()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Record()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Record(IFormCollection datas)
        {
            int id = int.Parse(datas["id"]);
            string name = datas["name"].ToString();
            string surname = datas["surname"].ToString();
            string fullName = name + "_" + surname;
            var files = HttpContext.Request.Form.Files;

            if(files != null )
            {
                foreach( var file in files )
                {
                    var fileName = file.FileName;
                    var fileExtension = Path.GetExtension(fileName); // Dosya uzantısını (".txt, .jpeg vb.") çeker
                    var newFileName = string.Concat(fullName, fileExtension); // Fotoğrafın ismi belirlendi
                    var filePath = Path.Combine(_environment.WebRootPath, "CameraPhotos") + $@"\{newFileName}"; //Dosya yolu
                    var fPath = "/CameraPhotos/" + newFileName;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        StoreInFolder(file, filePath);
                    }

                    if (filePath != null)
                    {
                        StoreInDatabase(id, fullName, fPath);
                    }
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        private void StoreInFolder(IFormFile file, string filePath)
        {
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }
        private void StoreInDatabase(int id, string fullName, string filePath)
        {

            ImageStore imageStore = new ImageStore()
            {
                Id= id,
                Name_Surname = fullName,
                Image = filePath
            };

            _databaseContext.ImageStores.Add(imageStore);
            _databaseContext.SaveChanges();


        }
        public IActionResult RollCall()
        {
            List<ImageStore> list = _databaseContext.ImageStores.ToList();
            return View(list);
        }
    }
}
