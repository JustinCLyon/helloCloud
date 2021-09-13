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
            List<Fruit> fruitModel = Fruit.GetFruitModelList(GetFruitList());

            return View("ListFruits", fruitModel);
        }

        public IActionResult Edit( int id)
        {
            List<Fruit> fruitModel = Fruit.GetFruitModelList(GetFruitList());
            Fruit fruitEdit = new Fruit();

            foreach (Fruit fruit in fruitModel)
            {
                if (fruit.Id == id)
                {
                    fruitEdit = fruit;
                    break;
                } 
            }

            return View("EditFruit", fruitEdit);
        }

        [HttpPost]
        public IActionResult Edit(Fruit fruit)
        {
            fruits updatedFruitList = Fruit.EditFruit(GetFruitList(), fruit);

            serializeFruit(updatedFruitList);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View("CreateFruit");
        }

        [HttpPost]
        public IActionResult Create(Fruit newFruitModel)
        {

            //obtain fruit XSD class
            fruits fruitClass = GetFruitList();

            //convert fruit items from array to list
            List<fruitsFruit> fruitList = fruitClass.fruit.ToList<fruitsFruit>();

            //Create fruitsFruit item to store the newly created fruit
            fruitsFruit newFruit = new fruitsFruit();
            newFruit.name = newFruitModel.Name;
            newFruit.family = newFruitModel.Family;
            newFruit.harvestseason = newFruitModel.HarvestSeason;
            newFruit.shape = newFruitModel.Shape;

            //add new fruit to list and convert back to array
            fruitList.Add(newFruit);
            fruitClass.fruit = fruitList.ToArray();

            //serialize the updated fruit class
            serializeFruit(fruitClass);


            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            //obtain fruit XSD class
            fruits fruitClass = GetFruitList();

            //convert fruit items from array to list
            List<fruitsFruit> fruitList = fruitClass.fruit.ToList<fruitsFruit>();

            //index for matching ids
            int i = 1;
            foreach (fruitsFruit fruit in fruitList)
            {
                if (i == id)
                {
                    fruitList.Remove(fruit);
                    break;
                }
                i++;
            }

            fruitClass.fruit = fruitList.ToArray();

            serializeFruit(fruitClass);

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

        public fruits GetFruitList()
        {
            string rootPath = this.Environment.ContentRootPath;
            string xmlFilePath = rootPath + "/Data/fruits.xml";

            fruits fruitList = null;

            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(fruits));
                fruitList = (fruits)serializer.Deserialize(fs);
                fs.Close();
            }
            return fruitList;
        }

        //Take edited fruit model, update the xml schema class with the new data, and serialize it back into xml.
        public void serializeFruit(fruits fruitList)
        {
            string rootPath = this.Environment.ContentRootPath;
            string xmlFilePath = rootPath + "/Data/fruits.xml";

            using(FileStream fs = new FileStream(xmlFilePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(fruits));
                serializer.Serialize(fs, fruitList);
                fs.Close();
            }



        }
    }
}
