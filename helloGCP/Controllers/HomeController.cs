using helloGCP.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace helloGCP.Controllers
{
    public class HomeController : Controller
    {

        private IWebHostEnvironment Environment;

        private readonly ILogger<HomeController> _logger;

        public HomeController(IWebHostEnvironment _environment, ILogger<HomeController> logger)
        {
            Environment = _environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<FruitViewModel> fruitModel = FruitViewModel.GetFruitModelList(GetFruitList());

            return View("ListFruits", fruitModel);
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

        public fruits GetFruitList()
        {
            string rootPath = this.Environment.ContentRootPath;
            string xmlFilePath = rootPath + "/Data/Fruits.xml";

            fruits fruitList = null;

            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(fruits));
                fruitList = (fruits)serializer.Deserialize(fs);
                fs.Close();
            }
            return fruitList;
        }
    }
}
