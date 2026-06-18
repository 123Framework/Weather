using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controllers
{
   
    public class WeatherController : Controller
    {
       public IActionResult Index()
        {
            return View();
        }

    }
}
