using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caloryfi.Model;

namespace Caloryfi.Model
{
    public class FoodModel: IngriedentsModel
    {
        public double Weight { get; set; }
        public double FoodKcal 
        {
            get
            {
                return Kcal;
            }
        }
        public double FoodProteins
        {
            get
            {
                return Proteins;
            }
        }
        public double FoodCarbs
        {
            get
            {
                return Carbs;
            }
        }
        public double FoodFats
        {
            get
            {
                return Fats;
            }
        }
    }
}
