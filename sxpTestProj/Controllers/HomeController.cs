using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using sxpTestProj.Models;
using sxpTestProj.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sxpTestProj.Controllers
{
    public class HomeController : Controller
    {

        public IWebHostEnvironment _appEnvironment;
        public HomeController(IWebHostEnvironment env)
        {
            _appEnvironment = env;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendFile(IFormFile file)
        {
            List<Unit> units = new List<Unit>();
            if (file != null)
            {
                var dateStr = DateTime.Now.ToString().Replace(".", "").Replace(" ", "").Replace(":", "");
                string[] permittedextensions = { ".xml" };
                string path = "/Files/" + dateStr + file.FileName;
                var ext = Path.GetExtension(path);
                if (!permittedextensions.Contains(ext))
                {
                    return RedirectToAction("Error");
                }
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                units = XmlParse.Parse(_appEnvironment.WebRootPath + path);
            }
            List<string> Titles = new List<string>();
            foreach (var t in units)
            {
                Titles.Add(t.Title);
            }
            ViewBag.Units = units;
            ViewBag.Titles = units.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToList();
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
