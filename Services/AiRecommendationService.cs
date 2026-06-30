using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WeatherApp.Services
{
    public class AiRecommendationService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public AiRecommendationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> GetRecommendationAsync(double temperature, string description, double windSpeed)
        {
            string apiKey = _configuration["OpenRouter:ApiKey"];
            string model = _configuration["OpenRouter:Model"]??"openai/gpt-4o-mini";

            string prompt = $"ты ии-ассистент по погоде и одежде дай короткую полезную рекомендацию на русском языке." +
                $"Погода: температура: {temperature} C" +
                $"Описание: {description}" +
                $"Ветер: {windSpeed} м/с" +
                $"Ответ раздели на 1 что надеть 2 что взять с собой" +
                $"совет по ситуации";

            var requestBody = new
            {
                model = model,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                },
                temperature = 0.7
            };


            string json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            request.Headers.Add("HTTP-Referer", "http://localhost");
            request.Headers.Add("X-Title", "Weather AI ASistant");

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(responseText);
                return "Не удалось получить AI-рекомендацию";
            }
            else { 
            using var document = JsonDocument.Parse(responseText);
                return document.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
            }
        }

    }
}
