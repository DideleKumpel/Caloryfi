using Caloryfi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // zrob funkcje do pobierania i aktualizowania ustawien uzytkownika z API
    }
}
