using frontendLossSounds.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;


namespace frontendLossSounds.Services
{
    
    public class BaseServices

    {
        public async Task<TokenModel> GetToken()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            TokenModel tokenModel = new TokenModel();

            try
            {
                var uri = configuration["URL_Base_API"];
                var idApp = configuration["ID_App"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                string url = string.Format("{0}/api/{1}?ID_App={2}", uri, "GetToken", idApp);
                var response= await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse= await response.Content.ReadAsStringAsync();
                    var token = JsonSerializer.Deserialize<TokenModel>(jsonResponse);
                    return token;
                }
                else
                {
                    throw new Exception("Error al solicitar el token");
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            
            }

            return tokenModel;
        }
    }
}
