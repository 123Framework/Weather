namespace WeatherApp.Models
{
    public class WeatherApiResponse
    {
        public MainInfo Main { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public WindInfo Wind { get; set; }
    }
    public class MainInfo
    {
        public double Temp { get; set; }
    }
    public class WeatherInfo
    {
        public string Description { get; set; }
    }
    public class WindInfo
    {
        public double Speed { get; set; }
    }


}

