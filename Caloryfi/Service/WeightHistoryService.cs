using Caloryfi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Service
{
    public class WeightHistoryService
    {
        private readonly HttpClient _httpClient;
        private WeightHistoryModel _currentWeight;
        public WeightHistoryModel CurrentWeight { get { return _currentWeight; } }

        public WeightHistoryService(HttpClient httpClient) { 
            _httpClient = httpClient;
        }

        public async Task<(bool success, string message)> GetCurrentWeightAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/WeightHistory/GetCurrentWeight");
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to fetch current weight");
                WeightHistoryModel weight = await response.Content.ReadFromJsonAsync<WeightHistoryModel>();
                if (weight == null)
                {
                    return (false, "Invalid response from server");
                }
                _currentWeight = weight;
                return (true, "Current weight downloaded and set");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> UpdateCurrentWeightAsync(int newWeight)
        {
            try
            {
                var response = await _httpClient.PostAsync($"/api/WeightHistory/UpdateWeight/{newWeight}", null);
                if (response.IsSuccessStatusCode)
                {
                    _currentWeight.Weight = newWeight;
                    return (true, "Current weight updated successfully");
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
