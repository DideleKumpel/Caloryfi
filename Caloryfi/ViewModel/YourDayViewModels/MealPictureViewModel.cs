using Caloryfi.Model;
using Caloryfi.Views.DialogPopups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel.YourDayViewModels;

public partial class MealPictureViewModel : ObservableObject
{
    [ObservableProperty] string _name;
    [ObservableProperty] string _weight;

    [ObservableProperty] string _kcal;
    [ObservableProperty] string _proteins;
    [ObservableProperty] string _carbs;
    [ObservableProperty] string _fats;

    private ImageModel _mealImage;

    private static readonly HashSet<string> allowedExtensions = new HashSet<string>
    {
        ".jpg", ".jpeg", ".png"
    };

    public MealPictureViewModel()
    {
        // TODO: add meal to list
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

    }

    [RelayCommand]
    void AddMeal()
    {

    }
    
}
