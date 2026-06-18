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
    }
}
