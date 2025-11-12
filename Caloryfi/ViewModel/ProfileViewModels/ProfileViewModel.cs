using Caloryfi.Model;
using Caloryfi.View.ProfileViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel.ProfileViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly IServiceProvider _service;

        [ObservableProperty]
        private double _currentWeight;
        [ObservableProperty]
        private ObservableCollection<WeightHistoryModel> _weightHisotry;

        public ProfileViewModel(IServiceProvider Service)
        {
            _service = Service;
        }

        [RelayCommand]
        private void Logout()
        {

        }

        [RelayCommand]
        private void SwitchToSettingsPage()
        {
            Shell.Current.GoToAsync(nameof(SettingsView));
        }
    }
}
