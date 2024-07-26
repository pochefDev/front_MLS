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

        //[Authorize(Role.DEFAULT)]
        public async Task<IActionResult> Index()
        {
            //var weather = await _weatherService.GetWeather("Aguascalientes");
            List<CancionData> songs = await _musicServices.GetSongs(null);
            List<AlbumInfo> albums = await _musicServices.GetAlbumsInfo();

            songs = songs.Take(10).ToList();
            foreach (var song in songs)
            {
                song.Caratula_Cancion_Base64 = Convert.ToBase64String(song.Caratula_Cancion);
            }

            foreach(var album in albums)
            {
                album.Caratula_Album_Base64 = Convert.ToBase64String(album.Caratula_Album);
            }

            ViewBag.RecommendedSongs = songs;
            ViewBag.Albums = albums;
            ViewBag.Weather = null;//weather;

            return View();
        }



        #region Resources

        #region Songs
        //[Authorize(Role.DEFAULT)]
        public async Task<IActionResult> GetSongData(int ID_Cancion)
        {
            byte[] song = await _musicServices.GetSongData(ID_Cancion);


            return File(song, "audio/flac", $"cancion.flac");
        }

        //[Authorize(Role.DEFAULT)]
        public async Task<JsonResult> GetSongInfo(int ID_Cancion)
        {
            List<CancionData> song = await _musicServices.GetSongInfo(ID_Cancion);

            return Json(song.FirstOrDefault());
        }

        #endregion

        #region Albums

        public async Task<JsonResult> GetAlbumsInfo()
        {
            List<AlbumInfo> albums = await _musicServices.GetAlbumsInfo();

            return Json(albums);
        }

        #endregion

        #endregion
    }
}
