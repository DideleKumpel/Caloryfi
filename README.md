# Caloryfi

A cross-platform calorie and macro tracking app built with **.NET MAUI**. Log your meals, track calories, carbs, protein and fat against your daily goals, and let AI estimate the nutritional values of a meal from a photo.

> This repo contains the mobile/desktop app. The backend lives in **[CaloryfiAPI](https://github.com/DideleKumpel/CaloryfiAPI)**, an ASP.NET Core Web API with PostgreSQL and Google Gemini.

## Features

- **Accounts.** Register and log in with JWT authentication. The token is kept in the device's secure storage, so you stay signed in between launches.
- **Automatic daily targets.** At sign-up, your daily calorie target is calculated from your weight, sex, activity level and diet goal. You can adjust it later in Settings.
- **Your Day.** See today's meals with a live summary of calories and macros.
- **Meal logging:**
  - search the ingredient database and add ingredients by weight (grams);
  - edit an ingredient's weight or remove it from a meal;
  - add and delete meals.
- **Custom ingredients.** Create your own ingredients with kcal, carbs, protein and fat per 100 g. You can enter the values yourself or tap **Auto calculate makro** to have AI fill them in.
- **Meal from a photo.** Take a picture or load one from the gallery, and AI estimates the meal's ingredients and nutritional values.
- **History.** Browse past days on a calendar and open any meal to see its details.
- **Profile:**
  - track your current weight;
  - change your username, e-mail or password.
- **Settings.** Set your sex, diet goal, activity level, number of meals per day, and daily kcal and macro targets.

## Tech stack

| | |
| --- | --- |
| Framework | [.NET 9](https://dotnet.microsoft.com/) · [.NET MAUI](https://learn.microsoft.com/dotnet/maui/) |
| Architecture | MVVM with [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/) and dependency injection |
| UI helpers | [CommunityToolkit.Maui](https://learn.microsoft.com/dotnet/communitytoolkit/maui/) (popups, converters) |
| Networking | `HttpClient` + JSON (System.Net.Http.Json, Newtonsoft.Json) |
| Backend | [CaloryfiAPI](https://github.com/DideleKumpel/CaloryfiAPI): ASP.NET Core 8 Web API, EF Core, PostgreSQL, JWT |
| AI | Google Gemini (`gemini-2.5-flash-lite`), called by the backend |
| Platforms | Android · iOS · macOS (Mac Catalyst) · Windows |

## Project structure

```
Caloryfi/
├── Model/              # data models and DTOs (meals, ingredients, user, settings…)
├── View/               # XAML pages
│   ├── YourDayViews/   # today's meals, add food, custom ingredient, meal photo
│   ├── HistoryViews/   # calendar history and meal details
│   ├── ProfileViews/   # profile and settings
│   └── DialogPopup/    # message / confirmation / input popups
├── ViewModel/          # one view model per page (same folder layout as View/)
├── Service/            # API clients: User, Meal, MealComponent, Ingredients, AI, …
├── Converters/         # XAML value converters
├── CustomControls/     # reusable controls
├── Resources/          # styles, colours, fonts, icons, splash screen
├── Platforms/          # platform-specific code (Android, iOS, MacCatalyst, Windows)
└── MauiProgram.cs      # DI registration and API configuration
```

## Getting started

Running Caloryfi takes two parts: the **API** (backend) and the **app**.

### 1. Run the API

Full source: **[DideleKumpel/CaloryfiAPI](https://github.com/DideleKumpel/CaloryfiAPI)**

**Requirements**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- A [Google Gemini API key](https://aistudio.google.com/apikey), used for the AI features

```bash
git clone https://github.com/DideleKumpel/CaloryfiAPI.git
cd CaloryfiAPI/CaloryfiAPI
```

`appsettings.json` is not committed, so create it in `CaloryfiAPI/CaloryfiAPI/`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=caloryfi;Username=postgres;Password=YOUR_DB_PASSWORD"
  },
  "JwtConfig": {
    "Issuer": "CaloryfiAPI",
    "Audience": "Caloryfi",
    "Key": "A_LONG_RANDOM_SECRET_AT_LEAST_32_CHARACTERS"
  },
  "AiApiKey": {
    "Key": "YOUR_GEMINI_API_KEY"
  },
  "Logging": {
    "LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" }
  },
  "AllowedHosts": "*"
}
```

Create the database and start the server:

```bash
dotnet tool install --global dotnet-ef   # once
dotnet ef database update                # applies the migrations
dotnet run
```

The API runs on `http://localhost:5000` (and `https://localhost:5001`). In development you can explore and test every endpoint in Swagger at **http://localhost:5000/swagger**.

### 2. Run the app

**Requirements**
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- The MAUI workload: `dotnet workload install maui`
- For each platform you want to build:
  - **Android:** Android SDK and an emulator or device.
  - **iOS / Mac Catalyst:** a Mac with Xcode.
  - **Windows:** Windows 10 (build 19041) or later.

Visual Studio 2022 (17.14+) with the **.NET Multi-platform App UI** workload installs all of this for you.

**Point the app at your API** in `Caloryfi/MauiProgram.cs`:

```csharp
var apiBaseUrl = "https://your-api-url";
```

| Where the app runs | Use |
| --- | --- |
| Windows (same PC as the API) | `http://localhost:5000` |
| Android emulator | `http://10.0.2.2:5000` (the emulator's alias for your PC) |
| Physical phone | A public tunnel to your PC, e.g. [ngrok](https://ngrok.com/): `ngrok http 5000`, then use the `https://….ngrok-free.dev` URL it prints |

> Android blocks plain `http://` traffic by default. For the emulator options above, either use the ngrok HTTPS URL or allow cleartext traffic for development.

Then build and run:

```bash
git clone https://github.com/DideleKumpel/Caloryfi.git
cd Caloryfi

# Android
dotnet build Caloryfi/Caloryfi.csproj -t:Run -f net9.0-android

# Windows
dotnet build Caloryfi/Caloryfi.csproj -t:Run -f net9.0-windows10.0.19041.0
```

You can also open `Caloryfi.sln` in Visual Studio, pick a target device and press **F5**.

> **Password rules:** at least 12 characters, with at least one uppercase letter and one digit.

## API overview

Endpoints of [CaloryfiAPI](https://github.com/DideleKumpel/CaloryfiAPI) used by the app. All except login and registration need an `Authorization: Bearer <token>` header. Tokens are valid for 3 hours and are refreshed when the app starts.

| Area | Endpoints |
| --- | --- |
| User | `POST /api/User/Login`, `POST /api/User/CreateAccount`, `GET /api/User/RefreshToken`, `GET /api/User/GetProfile`, `POST /api/User/ChangeUsername`, `ChangeEmail`, `ChangePassword` |
| Settings | `GET /api/Setting/GetSettings`, `POST /api/Setting/UpdateSettings` |
| Meals | `GET /api/MealList/GetMeals?date=yyyy-MM-dd`, `POST /api/MealList/AddMeal`, `DELETE /api/MealList/DeleteMeal/{id}` |
| Meal components | `POST /api/MealComponent/AddComponent`, `POST /api/MealComponent/UpdateWeight`, `DELETE /api/MealComponent/DeleteComponent/{ingredientId}/{mealId}` |
| Ingredients | `GET /api/Ingredients/GetIngrediets`, `POST /api/Ingredients/AddCustomIngredient` |
| Weight | `GET /api/WeightHistory/GetCurrentWeight`, `POST /api/WeightHistory/UpdateWeight/{weight}` |
| AI (Gemini) | `GET /api/GeminiApi/AutoCalculateIngredientmMakro/{name}`, `POST /api/GeminiApi/GetFoodFromImage` |
