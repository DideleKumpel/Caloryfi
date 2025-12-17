using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Caloryfi.Model.DTO;

namespace Caloryfi.Service
{
    public class AIService
    {
        private readonly HttpClient _httpClient;

        public AIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string message)> AutoCalculateIngredientmMakroAsync(string ingredeintName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/GeminiApi/AutoCalculateIngredientmMakro/{ingredeintName}");
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to fetch AI response");
                var result = await response.Content.ReadAsStringAsync();
                return (true, result);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> GetFoodFromImage(FoodFormImageDTO foodFromImageDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/api/GeminiApi/GetFoodFromImage", foodFromImageDTO);
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to fetch AI response");
                var result = await response.Content.ReadAsStringAsync();
                return (true, result);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
