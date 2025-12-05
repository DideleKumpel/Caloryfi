using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Caloryfi.Model;

namespace Caloryfi.Service
{
    public class IngredientsService 
    {
        private readonly HttpClient _httpClient;

        public IngredientsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string message)> GetIngredientsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Ingredients/GetIngrediets");
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to fetch ingredients");
                var result = await response.Content.ReadAsStringAsync();
                return (true, result);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> AddCustomIngredientAsync(IngriedentsModel newIngredient)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Ingredients/AddCustomIngredient", newIngredient);
                if (!response.IsSuccessStatusCode)
                    return (false, "Failed to add custom ingredient");
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
