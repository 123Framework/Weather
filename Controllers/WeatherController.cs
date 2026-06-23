using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
   
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;
        private readonly AiRecommendationService _aiService;


        public WeatherController(
            WeatherService weatherService, AiRecommendationService aiService)
        {
            _weatherService = weatherService;
            _aiService = aiService;
        }

        [HttpGet]
       public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            if (weather == null) {
                ViewBag.Error = "Не удалось получить проноз. проевдите интернет, api-key , или другой город";
                return View();  
            }
            weather.Recommendation = _aiService.GetRecommendation(weather.Temperature, weather.Description, weather.WindSpeed);

            return View(weather);
        }

    }
}
