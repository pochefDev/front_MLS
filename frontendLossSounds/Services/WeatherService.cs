using frontendLossSounds.Models;

namespace frontendLossSounds.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeather:ApiKey"];
        } 


        public async Task<WeatherResponse> GetWeather(string City)
        {
            WeatherResponse response = await _httpClient.GetFromJsonAsync<WeatherResponse>($"https://api.openweathermap.org/data/2.5/weather?q={City}&appid={_apiKey}");

            return response;
        }
    }
}
