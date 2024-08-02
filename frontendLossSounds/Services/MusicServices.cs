using frontendLossSounds.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace frontendLossSounds.Services
{
    public class MusicServices
    {

        #region Configurations
        private readonly BaseServices _services;

        public MusicServices()
        {
            _services = new BaseServices();
        }
        #endregion

        #region Songs Services
        public async Task<dynamic> GetSongs(int? ID_Cancion)
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = $"api/Canciones?ID_Cancion={ID_Cancion}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<List<CancionData>>(jsonResponse);
                    return responseObject;
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<dynamic> GetSongData(int ID_Cancion)
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = $"api/GetSongData?ID_Cancion={ID_Cancion}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<byte[]>(jsonResponse);
                    return responseObject;
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<dynamic> GetSongInfo(int ID_Cancion)
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = $"api/Canciones?ID_Cancion={ID_Cancion}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<List<CancionData>>(jsonResponse);
                    return responseObject;
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Role.IT_ADMIN)]
        public async Task<ResponseData> SaveMetadataSong(string archivo)
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];
                //var idApp = configuration["ID_App"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = "/api/SaveSong";

                string jsonContent = JsonSerializer.Serialize(archivo);

                StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PostAsync(url, stringContent);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    return new ResponseData { Success = true, Message = jsonResponse };
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return new ResponseData { Success = false, Message = jsonResponse };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Role.IT_ADMIN)]
        public async Task<ResponseData> DeleteSong(int ID_Song)
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];
                //var idApp = configuration["ID_App"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = "/api/DeleteSong";

                string jsonContent = JsonSerializer.Serialize(ID_Song);

                StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PostAsync(url, stringContent);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    return new ResponseData { Success = true, Message = jsonResponse };
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return new ResponseData { Success = false, Message = jsonResponse };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Album Services

        public async Task<dynamic> GetAlbumsInfo()
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = $"api/GetAlbumsInfo";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<List<AlbumInfo>>(jsonResponse);
                    return responseObject;
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return jsonResponse;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
