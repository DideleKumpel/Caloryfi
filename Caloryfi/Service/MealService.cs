using Caloryfi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Service
{
    public class MealService
    {
        private readonly HttpClient _httpClient;


        public MealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string message)> GetToDaysMeals()
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/MealList/GetMeals?date={DateTime.Today:yyyy-MM-dd}");
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to fetch meals for today");
                var result = await response.Content.ReadAsStringAsync();
                return (true, result);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool succes, string message)> AddNewMealAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("/api/MealList/AddMeal", null);
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to create new meal");
                var result = await response.Content.ReadAsStringAsync();
                return (true, result);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
        
        public async Task<(bool success, string message)> DeleteMealAsync(int mealId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/MealList/DeleteMeal/{mealId}");
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to delete meal");
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
