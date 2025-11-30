using Caloryfi.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Service
{
    public class MealComponentService
    {
        private readonly HttpClient _httpClient;

        public MealComponentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool succes, string message)> DeleteMealComponentAsync(int mealID, int ingredientID)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/MealComponent/DeleteComponent/{ingredientID}/{mealID}");
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to delete meal component");
                var result = await response.Content.ReadAsStringAsync();
                return (true, result);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> UpdateMealComponentWeightAsync(MealComponentDTO updatedMeal)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/MealComponent/UpdateWeight", updatedMeal);
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to update meal component weight");
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
