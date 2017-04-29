namespace UploadFile.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Hosting;
    using System.IO;

    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("api/uploadfile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            string fileName = string.Empty;

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    fileName = file.FileName;

                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return Ok(new
            {
                url = "http://localhost:64268/uploads/"+fileName
            });
        }
    }
}