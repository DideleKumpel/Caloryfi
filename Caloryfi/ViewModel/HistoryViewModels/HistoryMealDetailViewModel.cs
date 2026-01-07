using Caloryfi.Model;
using Caloryfi.Service;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel.HistoryViewModels
{
    public partial class HistoryMealDetailViewModel: ObservableObject
    {
        [ObservableProperty]
        private MealModel _meal;

        public HistoryMealDetailViewModel()
        {
        }
    }
}
