using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Model
{
    public partial class MealModel: ObservableObject
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

        [ObservableProperty]
        private ObservableCollection<FoodModel> _ingredients;

        partial void OnIngredientsChanged(ObservableCollection<FoodModel> oldValue,
                                          ObservableCollection<FoodModel> newValue)
        {
            if (oldValue != null)
                Unsubscribe(oldValue);

            if (newValue != null)
                Subscribe(newValue);

            RaiseTotalsChanged();
        }

        private void Subscribe(ObservableCollection<FoodModel> collection)
        {
            collection.CollectionChanged += IngredientsChanged;

            foreach (var food in collection)
                food.PropertyChanged += IngredientChanged;
        }

        private void Unsubscribe(ObservableCollection<FoodModel> collection)
        {
            collection.CollectionChanged -= IngredientsChanged;

            foreach (var food in collection)
                food.PropertyChanged -= IngredientChanged;
        }

        private void IngredientsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (FoodModel f in e.NewItems)
                    f.PropertyChanged += IngredientChanged;

            if (e.OldItems != null)
                foreach (FoodModel f in e.OldItems)
                    f.PropertyChanged -= IngredientChanged;

            RaiseTotalsChanged();
        }

        private void IngredientChanged(object sender, PropertyChangedEventArgs e)
        {
            // interesuje nas tylko zmiana wagi lub wyliczanych makr food
            if (e.PropertyName == nameof(FoodModel.Weight) ||
                e.PropertyName == nameof(FoodModel.FoodKcal) ||
                e.PropertyName == nameof(FoodModel.FoodProteins) ||
                e.PropertyName == nameof(FoodModel.FoodCarbs) ||
                e.PropertyName == nameof(FoodModel.FoodFats))
            {
                RaiseTotalsChanged();
            }
        }

        private void RaiseTotalsChanged()
        {
            OnPropertyChanged(nameof(Calories));
            OnPropertyChanged(nameof(Carbs));
            OnPropertyChanged(nameof(Proteins));
            OnPropertyChanged(nameof(Fats));
        }
    }
}
