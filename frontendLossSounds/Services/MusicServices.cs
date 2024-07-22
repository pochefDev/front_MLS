using frontendLossSounds.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<dynamic> GetSongs(int? ID_Cancion, bool? fileContent)
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
                string url = $"api/Canciones?ID_Cancion={ID_Cancion}&fileContent={fileContent}";

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
    }
}
