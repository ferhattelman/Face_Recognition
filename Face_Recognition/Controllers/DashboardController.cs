using Face_Recognition.EntityStore;
using Face_Recognition.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var degerler=_databaseContext.Lessons.ToList();
            return View(degerler);
        }
        public IActionResult Lists()
        {
            var model = new MyViewModel
            {
                ProgramlamaListesi = _databaseContext.Programlama.ToList(),
                LineerCebirsListesi = _databaseContext.LineerCebirs.ToList(),
                IktisatListesi = _databaseContext.Iktisat.ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Record()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> lessons =(from i in _databaseContext.Lessons.ToList()
                                           select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.Lessons = lessons;
            return View();
        }

        [HttpPost]
        public IActionResult Record(IFormCollection datas, string Name)
        {
            int id = int.Parse(datas["id"]);
            string names = datas["names"].ToString();
            string surname = datas["surname"].ToString();
            string fullName = names + "_" + surname;
            var files = HttpContext.Request.Form.Files;


            if (files != null )
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
                        ViewBag.message = "Registration Successful";
                    }
                    else
                    {
                        ViewBag.message = "The person already exists";
                    }
                    if (filePath != null)
                    {
                        StoreInDatabase(id, fullName, fPath);
                        LessonName(Name,id,fullName, fPath);
                    }
                    else
                    {
                        ViewBag.message = "The person already exists";
                    }
                }
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        public void LessonName(string Name, int id, string fullName, string filePath)
        {

            if(Name == "1")
            {
                LineerCebir l = new LineerCebir
                {
                    Id = id,
                    Name_Surname = fullName,
                    Image = filePath
                };
                _databaseContext.LineerCebirs.Add(l);
                _databaseContext.SaveChanges();
            }

            else if (Name == "2")
            {
                Programlama pro= new Programlama
                {
                    Id = id,
                    Name_Surname = fullName,
                    Image = filePath
                };
                _databaseContext.Programlama.Add(pro);
                _databaseContext.SaveChanges();
            }

            else if(Name == "3")
            {
                Iktisat i = new Iktisat
                {
                    Id = id,
                    Name_Surname = fullName,
                    Image = filePath
                };
                _databaseContext.Iktisat.Add(i);
                _databaseContext.SaveChanges();
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

        [HttpPost]
        public IActionResult RollCall(string id)
        {
            int num = Convert.ToInt32(id);
            var toDelete = _databaseContext.ImageStores.FirstOrDefault(x => x.Id == num);
            var toDelete1 = _databaseContext.LineerCebirs.FirstOrDefault(x => x.Id == num);
            var toDelete2 = _databaseContext.Iktisat.FirstOrDefault(x => x.Id == num);

            if (toDelete != null)
            {
                var imagePath = Path.Combine(_environment.WebRootPath, "CameraPhotos", toDelete.Name_Surname);

                // İlk olarak dosyayı sil
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                // Daha sonra veritabanından kaydı sil
                _databaseContext.ImageStores.Remove(toDelete);
                _databaseContext.LineerCebirs.Remove(toDelete1);
                _databaseContext.Iktisat.Remove(toDelete2);
                _databaseContext.SaveChanges();

                ViewBag.Message = "Kayıt başarıyla silindi.";
            }
            else
            {
                ViewBag.Message = "Kayıt bulunamadı veya silinemedi.";
            }

            return View();

        }
    }
}
