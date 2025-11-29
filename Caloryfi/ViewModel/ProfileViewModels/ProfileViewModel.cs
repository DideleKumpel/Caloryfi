using Caloryfi.Model;
using Caloryfi.Service;
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
        private readonly WeightHistoryService _weightHistoryService;

        [ObservableProperty]
        private double _currentWeight;
        [ObservableProperty]
        private ObservableCollection<WeightHistoryModel> _weightHisotry;
        [ObservableProperty]
        private bool _weightUpdateMessageIsVisible;
        [ObservableProperty]
        private string _weightUpdateMessage;

        public ProfileViewModel(IServiceProvider Service, WeightHistoryService weightHistoryService)
        {
            _service = Service;
            _weightHistoryService = weightHistoryService;

            WeightUpdateMessageIsVisible = false;
            CurrentWeight = _weightHistoryService.CurrentWeight.Weight;
        }

        [RelayCommand]
        private void Logout()
        {

        }

        [RelayCommand]
        private async void UpdateWeight()
        {
            var resault = await _weightHistoryService.UpdateCurrentWeightAsync((int)CurrentWeight);
            if (resault.success)
            {
                WeightUpdateMessage = "Weight updated successfully.";
                WeightUpdateMessageIsVisible = true;
            }
            else
            {
                WeightUpdateMessage = "Weight update failed.";
                WeightUpdateMessageIsVisible = true;
            }
        }

        [RelayCommand]
        private void SwitchToSettingsPage()
        {
            Shell.Current.GoToAsync(nameof(SettingsView));
        }
    }
}
