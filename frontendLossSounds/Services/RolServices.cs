using frontendLossSounds.Models;
using System.Text.Json;
using System.Text;
using frontendLossSounds.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace frontendLossSounds.Services
{
    public class RolServices
    {

        #region Configurations
        private BaseServices _services;
        public RolServices()
        {
            _services = new BaseServices();
        }
        #endregion

        #region Create
        [Authorize(Role.IT_ADMIN)]
        public async Task<ResponseData> CreateRol(string newRol)
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
                string url = "/api/CreateRol";

                string jsonContent = JsonSerializer.Serialize(newRol);

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

        #region Edit

        [Authorize(Role.IT_ADMIN)]
        public async Task<ResponseData> EditRol(string ID_Rol, RolModel EditRol)
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
                string url = $"/api/EditRol?ID_Rol={ID_Rol}";

                string jsonContent = JsonSerializer.Serialize(EditRol);

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

        #region Delete

        [Authorize(Role.IT_ADMIN)]
        public async Task<ResponseData> DeleteRol(string ID_Rol)
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
                string url = "/api/DeleteRol";

                string jsonContent = JsonSerializer.Serialize(ID_Rol);

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

    }
}
