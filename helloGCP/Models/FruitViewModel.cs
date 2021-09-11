using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace helloGCP.Models
{
    public class FruitViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Fruit")]
        public string Name { get; set; }

        [Display(Name = "Shape")]
        public string Shape { get; set; }

        [Display(Name = "Harvest Season")]
        public String HarvestSeason { get; set; }

        [Display(Name = "Family")]
        public string Family { get; set; }

        public static List<FruitViewModel> GetFruitModelList(fruits fruitList)
        {
            List<FruitViewModel> fruitViewList = new List<FruitViewModel>();
            int i = 1;

            foreach (fruitsFruit fr in fruitList.Items) 
            {
                FruitViewModel currentFruit = new FruitViewModel();

                currentFruit.Id = i;
                currentFruit.Name = fr.name;
                currentFruit.Shape = fr.shape;
                currentFruit.HarvestSeason = fr.harvestseason.ToString();
                currentFruit.Family = fr.family;

                fruitViewList.Add(currentFruit);
                i++;
            }
            return fruitViewList;
        }
    }
}
