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
        public double Wieght { get; set; }
        public double FoodKcal 
        { 
            get 
            {
                return (Wieght / 100) * Kcal;
            }
        }
        public double FoodProteins
        {
            get
            {
                return (Wieght / 100) * Proteins;
            }
        }
        public double FoodCarbs
        {
            get
            {
                return (Wieght / 100) * Carbs;
            }
        }
        public double FoodFats
        {
            get
            {
                return (Wieght / 100) * Fats;
            }
        }
    }
}
