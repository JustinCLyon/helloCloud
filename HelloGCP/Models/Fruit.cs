using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace helloGCP.Models
{
    public class Fruit
    {
        public int Id { get; set; }

        [Display(Name = "Fruit")]
        public string Name { get; set; }

        [Display(Name = "Shape")]
        public string Shape { get; set; }

        [Display(Name = "Harvest Season")]
        public fruitsFruitHarvestseason HarvestSeason { get; set; }

        [Display(Name = "Family")]
        public string Family { get; set; }

        public static List<Fruit> GetFruitModelList(fruits fruitList)
        {
            List<Fruit> fruitViewList = new List<Fruit>();
            int i = 1;

            foreach (fruitsFruit fr in fruitList.fruit) 
            {
                Fruit currentFruit = new Fruit();

                currentFruit.Id = i;
                currentFruit.Name = fr.name;
                currentFruit.Shape = fr.shape;
                currentFruit.HarvestSeason = fr.harvestseason;
                currentFruit.Family = fr.family;

                fruitViewList.Add(currentFruit);
                i++;
            }
            return fruitViewList;
        }

        public static fruits EditFruit(fruits fruitList, Fruit fruit)
        {
            fruitsFruit currentFruit = fruitList.fruit[fruit.Id - 1];

            currentFruit.name = fruit.Name;
            currentFruit.shape = fruit.Shape;
            currentFruit.harvestseason = fruit.HarvestSeason;
            currentFruit.family = fruit.Family;

            return fruitList;
        }
    }
}
