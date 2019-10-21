using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fileupload.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace fileupload.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleFile(IFormFile file)
        {
            string dir = _env.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            //var dir = _env.ContentRootPath;
            var name = file.FileName;
           using (var filestream = new FileStream(Path.Combine(dir, name), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(filestream);
            }
            return RedirectToAction("Index");
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
