namespace WeatherApp.Services
{
    public class AiRecommendationService
    {
        public string GetRecommendation(
            double temperature,
            string description,
            double windSpeed)
        {
            string advice = "";
            if (temperature < 0) advice += "Очень холодно. гаденьте зимнюю куртку, шапку и перчатки";
            else if (temperature < 10) advice += "Прохладно Рекомендутся теплая куртка";
            else if (temperature < 20) advice += "Комфортно. подойдет легкая куртка";
            else { advice += "Тепло. Можно надеть футболку"; }
            if (description.ToLower().Contains("rain")) advice += "Возьмите зонт";
            if (description.ToLower().Contains("snow")) advice += "Аккуратнее на скользкой дороге";
            if (windSpeed > 8) advice += "На улице сильный ветер";
            return advice;

        }

    }
}
