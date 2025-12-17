using Caloryfi.Model;
using Caloryfi.Model.DTO;
using Caloryfi.Service;
using Caloryfi.Views.DialogPopups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Caloryfi.ViewModel.YourDayViewModels;

public partial class MealPictureViewModel : ObservableObject
{
    private readonly AIService _aIService;
    private readonly IngredientsService _ingredientsService;
    private readonly MealComponentService _mealComponentService;

    private AddFoodViewModel _addFoodViewModel;
    public AddFoodViewModel AddFoodViewModel
    {
        set { _addFoodViewModel = value; }
    }

    [ObservableProperty] 
    private FoodModel _foodData;

    private ImageModel _mealImage;

    private static readonly HashSet<string> allowedExtensions = new HashSet<string>
    {
        ".jpg", ".jpeg", ".png"
    };

    public MealPictureViewModel(AIService aIService, IngredientsService ingredientsService, MealComponentService mealComponentService)
    {
        _aIService = aIService;
        _ingredientsService = ingredientsService;
        FoodData = new FoodModel();
        _mealComponentService = mealComponentService;
    }

    [RelayCommand]
    async void LoadPicture()
    {
        try
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Choose meal photo"
            });

            if (result == null)
                return;

            string fileExtension = System.IO.Path.GetExtension(result.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                var ErrorPopup = await Application.Current.MainPage.ShowPopupAsync(new MessagePopup("Invalid file type. Please select a JPG or PNG image."));
                return;
            }

            // Stream for imgage
            using var stream = await result.OpenReadAsync();

            // change to  byte[]
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();

            //save image to model
            _mealImage = new ImageModel
            {
                Extension = fileExtension,
                Data = imageBytes
            };
        }
        catch (Exception ex)
        {
            var ErrorPopup = await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(ex.Message));
        }
    }

    [RelayCommand]
    void DeletePicture ()
    {
        _mealImage = null;
    }

   [RelayCommand]
    async void EstymateMacros()
    {
        FoodFormImageDTO foodFormImageDTO = new FoodFormImageDTO
        {
            Name = FoodData.Name,
            Weight = FoodData.Weight,
            Image = _mealImage
        };
        var resoult = await _aIService.GetFoodFromImage(foodFormImageDTO);
        if (resoult.success)
        {
            try
            {
                var makroValues = JsonConvert.DeserializeObject<IngriedentsModel>(resoult.message);
                FoodData.Kcal = makroValues.Kcal;
                FoodData.Proteins = makroValues.Proteins;
                FoodData.Carbs = makroValues.Carbs;
                FoodData.Fats = makroValues.Fats;
            }
            catch (Exception ex)
            {
                var ErrorPopup = await Application.Current.MainPage.ShowPopupAsync(new MessagePopup("Failed to parse AI response."));
            }
        }
        else
        {
            var ErrorPopup = await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(resoult.message));
        }
    }

    [RelayCommand]
    async void AddMeal()
    {
        if (string.IsNullOrWhiteSpace(FoodData.Name))
        {
            await Application.Current.MainPage.ShowPopupAsync(new MessagePopup("Please enter a valid name for the meal."));
            return;
        }
        if (FoodData.Kcal <= 0 || FoodData.Proteins < 0 || FoodData.Carbs < 0 || FoodData.Fats < 0)
        {
            await Application.Current.MainPage.ShowPopupAsync(new MessagePopup("You nedd to estymate makro values first."));
            return;
        }
        if (FoodData.Weight < 0)
        {
            await Application.Current.MainPage.ShowPopupAsync(new MessagePopup("Please enter a valid weight for the meal."));
            return;
        }
        IngriedentsModel newIngredient = new IngriedentsModel
        {
            Name = FoodData.Name,
            Kcal = FoodData.Kcal,
            Proteins = FoodData.Proteins,
            Carbs = FoodData.Carbs,
            Fats = FoodData.Fats
        };
        var resoult = await _ingredientsService.AddCustomIngredientAsync(newIngredient); //adding ingredient to databse
        if (!resoult.success)
        {
            await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(resoult.message));
            return;
        }
        else
        {
            try
            {
                int newIngredientId = int.Parse(resoult.message); //getting the id of the newly added ingredient
                FoodData.Id = newIngredientId;
                MealComponentDTO newMealComponent = new MealComponentDTO
                {
                    MealId = _addFoodViewModel.CurrentMeal.Id,
                    IngredientId = FoodData.Id,
                    Weight = FoodData.Weight
                };
                var addFoodResoult = await _mealComponentService.AddMealComponentAsync(newMealComponent);
                if (addFoodResoult.success)
                {
                    _addFoodViewModel.CurrentMeal.Ingredients.Add(FoodData);
                    await Shell.Current.GoToAsync("../..");
                 }
                 else
                 { 
                    await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(addFoodResoult.message));
                 }
            }
            catch
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
    
}
