using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherService(
            HttpClient httpClient, IConfiguration configuration
            )
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<WeatherViewModel?> GetWeatherAsync(string city)
        {
            try
            {
                string apiKey = _configuration["WeatherApi:ApiKey"];
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
                var response = await _httpClient.GetAsync(url);

                var responseText = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseText);
                if (!response.IsSuccessStatusCode) return null;


                if (!response.IsSuccessStatusCode) return null;
                var json = await response.Content.ReadAsStringAsync();
                var weather = JsonSerializer.Deserialize<WeatherApiResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
                if (weather == null || weather.Main == null || weather.Weather == null || weather.Weather.Count == 0 || weather.Wind == null) return null;

                return new WeatherViewModel
                {
                    City = city,
                    Temperature = weather.Main.Temp,
                    Description = weather.Weather[0].Description,
                    WindSpeed = weather.Wind.Speed
                };
            }
            catch (Exception ex)
            {
                
                    Console.WriteLine("Weather API error");
                    Console.WriteLine(ex.Message);
                    return null;

                
            }
        }
    }
}
