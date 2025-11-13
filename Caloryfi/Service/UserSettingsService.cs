using Caloryfi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Service
{
    class UserSettingsService
    {
        private readonly HttpClient _httpClient;
        private UserSettingsModel _userSettings;   // tu zapisujesz ustawienia użytkownika po pobraniu ich z API beda dostpene dla kazdego viewmodelu nei bedzie trzeba pobierac ich wielokrotnie
        public UserSettingsModel UserSettings
        {
            get { return _userSettings; }
        }

        public UserSettingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string message)> GetUserSettingsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/UserSettings/Get");
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to fetch user settings");

                var result = await response.Content.ReadFromJsonAsync<UserSettingsModel>();
                if (result == null)
                    return (false, "Invalid response from server");

                _userSettings = result;
                return (true, "Settings loaded successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
        public async Task<(bool success, string message)> UpdateUserSettingsAsync(UserSettingsModel updatedSettings)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/api/UserSettings/Update", updatedSettings);

                if (response.IsSuccessStatusCode)
                {
                    _userSettings = updatedSettings;
                    return (true, "Settings updated successfully");
                }
                else
                {
                    string msg = await response.Content.ReadAsStringAsync();
                    return (false, $"Failed to update: {msg}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
