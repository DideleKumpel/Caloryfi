using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caloryfi.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Caloryfi.ViewModel.YourDayViewModels
{
    public partial class MealDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private MealModel _meal;

        [RelayCommand]
        private void Test()
        {
            Console.WriteLine("Test");
        }
    }
}
