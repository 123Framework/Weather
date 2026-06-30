namespace WeatherApp.Models
{
    public class WeatherApiResponse
    {
        public CurrentInfo Current {  get; set; }
        public LocationInfo Location { get; set; }

    }

    public class CurrentInfo
    {
        public double Temp_C { get; set; }

        public double Wind_Kph {  get; set; }
        public ConditionInfo Condition { get; set; }


    }
    public class ConditionInfo
    {
        public string Text { get; set; }
    }


    public class LocationInfo
    {
        public string Name { get; set; }
        public string Country { get; set; }



    }

}

