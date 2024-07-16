using frontendLossSounds.Services;
using Microsoft.AspNetCore.Mvc;

namespace frontendLossSounds.Controllers
{
    public class WeatherController : Controller
    {
        #region Configurations
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        #endregion

        public async Task<IActionResult> Index(string City)
        {
            var weather = await _weatherService.GetWeather("Aguascalientes");

            ViewBag.Weather = weather;

            return View(weather);
        }
    }
}
