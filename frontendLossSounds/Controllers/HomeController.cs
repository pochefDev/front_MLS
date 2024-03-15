using frontendLossSounds.Models;
using frontendLossSounds.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO; 
using Microsoft.Extensions.Configuration; 
using System.Net.Http;
using System;
using System.Text.Json.Serialization;

namespace frontendLossSounds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 

        private BaseServices _services;
        public HomeController(ILogger<HomeController> logger) 
        {
            _logger = logger;
            _services = new BaseServices();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel newUser)
        {
            TokenModel tokenModel = await _services.GetToken();
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];
                var idApp = configuration["ID_App"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = string.Format("/api/{0}","Register");


                string jsonContent = JsonSerializer.Serialize(newUser);

                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Json(jsonResponse);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Json(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user)
        {
            TokenModel tokenModel = await _services.GetToken();
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration configuration = builder.Build();

                var uri = configuration["URL_Base_API"];
                var idApp = configuration["ID_App"];

                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(uri)
                };

                client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                string url = string.Format("/api/{0}", "Login");


                string jsonContent = JsonSerializer.Serialize(user);

                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    //Guardar datos del usuario en variables de sesion
                    var responseObject = JsonSerializer.Deserialize<LoginResponseModel>(jsonResponse);

                    var userName = responseObject.Nombre_Usuario;
                    var Rol = responseObject.ID_Rol;

                    HttpContext.Session.SetString("UserName", userName);
                    HttpContext.Session.SetInt32("Rol", Rol);

                    return Json("Loggeo exitoso");
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return Json(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}


