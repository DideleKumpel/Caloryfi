using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
