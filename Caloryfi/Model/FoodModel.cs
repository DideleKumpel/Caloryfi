using Caloryfi.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Caloryfi.Model
{
    public partial class FoodModel: IngriedentsModel
    {
        [ObservableProperty]
        private double _weight;

        partial void OnWeightChanged(double oldValue, double newValue)
        {
            OnPropertyChanged(nameof(FoodKcal));
            OnPropertyChanged(nameof(FoodProteins));
            OnPropertyChanged(nameof(FoodCarbs));
            OnPropertyChanged(nameof(FoodFats));
        }
        public double FoodKcal 
        {
            get
            {
                return Kcal * (Weight / 100);
            }
        }
        public double FoodProteins
        {
            get
            {
                return Proteins * (Weight / 100);
            }
        }
        public double FoodCarbs
        {
            get
            {
                return Carbs * (Weight / 100);
            }
        }
        public double FoodFats
        {
            get
            {
                return Fats * (Weight / 100);
            }
        }
    }
}
