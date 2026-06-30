using System.Text.Json;
using WeatherApp.Models;
using static System.Net.WebRequestMethods;

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
                string url =  $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={city}&aqi=no";
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
                if (weather == null ) return null;

                return new WeatherViewModel
                {
                    City = weather.Location.Name,
                    Temperature = weather.Current.Temp_C,
                    Description = weather.Current.Condition.Text,
                    WindSpeed = Math.Round(weather.Current.Wind_Kph / 3.6,1)

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
