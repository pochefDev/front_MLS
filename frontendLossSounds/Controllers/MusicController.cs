using frontendLossSounds.Models;
using frontendLossSounds.Services;
using Microsoft.AspNetCore.Mvc;

namespace frontendLossSounds.Controllers
{
    public class MusicController : Controller
    {

        #region Configurations
        private readonly WeatherService _weatherService;

        public MusicController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        #endregion

        [Authorize(Role.DEFAULT)]
        public async Task<IActionResult> Index()
        {
            var weather = await _weatherService.GetWeather("Aguascalientes");

            ViewBag.Weather = weather;

            return View();
        }
    }
}
