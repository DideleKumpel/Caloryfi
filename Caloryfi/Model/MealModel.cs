using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Model
{
    public class MealModel
    {
        public int Id { get; set; }
        public int Calories { get {
                int totalCalories = 0;
                if (Ingredients != null)
                {
                    foreach (var item in Ingredients)
                    {
                        totalCalories += (int)item.FoodKcal;
                    }
                }
                return totalCalories;
            } 
        }
        public int Carbs { get {
                int totalCarbs = 0;
                if (Ingredients != null)
                {
                    foreach (var item in Ingredients)
                    {
                        totalCarbs += (int)item.FoodCarbs;
                    }
                }
                return totalCarbs;
            } }
        public int Proteins { get {
                int totalProteins = 0;
                if (Ingredients != null)
                {
                    foreach (var item in Ingredients)
                    {
                        totalProteins += (int)item.FoodProteins;
                    }
                }
                return totalProteins;
            } 
        }
        public int Fats { get {
                int totalFats = 0;
                if (Ingredients != null)
                {
                    foreach (var item in Ingredients)
                    {
                        totalFats += (int)item.FoodFats;
                    }
                }
                return totalFats;
            }
        }
        public virtual ObservableCollection<FoodModel> Ingredients { get; set; }
    }
}
