namespace WeatherApp.Models
{
    public class WeatherViewModel
    {
        public string City { get; set; } = "";
        public double Temperature { get; set; }
        public string Description { get; set; } = "";
        public double WindSpeed { get; set; }
        public string Recommendation { get; set; } = "";
    }
}
