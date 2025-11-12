using Caloryfi.Model;
using Caloryfi.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.Service
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private UserModel _userInfo { get; set; }
        public UserModel UserModel { get { return _userInfo; } }

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(bool success, string message)> CreateAccountAsync(string Username, string Email, string Password, int Weight, bool Sex)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/User/CreateAccount", new { Email = Email, Username = Username, Password = Password, Weight = Weight, Sex = Sex });
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return (true, content);
                }
                else
                {
                    return (false, content);
                }
            }
            catch (Exception e)
            {
                return (false, "Register failed: An error occurred while connecting to the server");
            }
        }

        public async Task<(bool success, string message)> LogInAsync(string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/User/Login", new { Email = email, Password = password });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                    if (result != null && result.Token != null)
                    {
                        await SecureStorage.SetAsync("AuthTokenKey", result.Token);
                        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
                        return (true, "Login successful");
                    }
                    else
                    {
                        return (false, "Login failed:Invalid login response");
                    }
                }
                else
                {
                    return (false, "Login failed: Wrong e-mail or password");
                }
            }
            catch (Exception e)
            {
                return (false, "Login failed: An error occurred while connecting to the server");
            }
        }

        public async Task<bool> RefreshToken(string OldToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OldToken);
                var response = await _httpClient.PostAsync("/api/Authorization/RefreshToken", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                    if (result != null && result.Token != null)
                    {
                        await SecureStorage.SetAsync("AuthTokenKey", result.Token);
                        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> GetUserInfoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/User/GetProfile");
                if (response.IsSuccessStatusCode)
                {
                    UserProfileData ProfileData = await response.Content.ReadFromJsonAsync<UserProfileData>();
                    _userInfo = new UserModel
                    {
                        Id = ProfileData.Id,
                        Email = ProfileData.Email,
                        Username = ProfileData.Username
                    };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private class LoginResult { public string Token { get; set; } }
    }
}
