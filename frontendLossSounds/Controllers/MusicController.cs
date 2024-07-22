using frontendLossSounds.Models;
using frontendLossSounds.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace frontendLossSounds.Controllers
{
    public class MusicController : Controller
    {

        #region Configurations
        private readonly WeatherService _weatherService;
        private readonly BaseServices _services;
        public readonly MusicServices _musicServices;

        public MusicController(WeatherService weatherService)
        {
            _weatherService = weatherService;
            _services = new BaseServices();
            _musicServices = new MusicServices();
        }
        #endregion

        [Authorize(Role.DEFAULT)]
        public async Task<IActionResult> Index()
        {
            //var weather = await _weatherService.GetWeather("Aguascalientes");
            List<CancionData> songs = await _musicServices.GetSongs(null, false);

            songs = songs.Take(10).ToList();
            foreach (var song in songs)
            {
                song.Caratula_Cancion_Base64 = Convert.ToBase64String(song.Caratula_Cancion);
            }

            ViewBag.RecommendedSongs = songs;
            ViewBag.Weather = null;//weather;

            return View();
        }



        #region Resources



        #endregion
    }
}
