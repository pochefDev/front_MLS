using frontendLossSounds.Models;
using frontendLossSounds.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace frontendLossSounds.Controllers
{
    public class HomeController : Controller
    {
        #region Configurations
        private readonly ILogger<HomeController> _logger; 

        private BaseServices _services;
        public HomeController(ILogger<HomeController> logger) 
        {
            _logger = logger;
            _services = new BaseServices();
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        
        public IActionResult Unathorized()
        {
            return View();
        }

        public PartialViewResult PrivacyPolicy()
        {
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Register

        [HttpPost]
        public async Task<ResponseData> Register(RegisterModel newUser)
        {
            var validatePass = _services.ValidatePassword(newUser.Contrasena);

            if (validatePass.Success)
            {
                try
                {

                    TokenModel tokenModel = await _services.GetToken();

                    #region Obtener recursos de appsettings.json
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    IConfiguration configuration = builder.Build();

                    var uri = configuration["URL_Base_API"];
                    var idApp = configuration["ID_App"];
                    #endregion

                    HttpClient client = new HttpClient()
                    {
                        BaseAddress = new Uri(uri)
                    };

                    client.DefaultRequestHeaders.Add("Authorization", tokenModel.Token);
                    string url = string.Format("/api/{0}", "Register");


                    string jsonContent = JsonSerializer.Serialize(newUser);

                    var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(url, stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        return new ResponseData{ Success = true, Message = jsonResponse };

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
            else
            {

                return new ResponseData { Success= false, Message = validatePass.Message };
            }
        }

        #endregion

        #region Login
        [HttpPost]
        public async Task<AccessModel> Login(LoginModel user)
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


                    var responseObject = JsonSerializer.Deserialize<LoginResponseModel>(jsonResponse);

                    Settings userLogged = new Settings
                    {
                        NombreUsuario = responseObject.Nombre_Usuario,
                        RolID = responseObject.ID_Rol
                    };

                    HttpContext.Session.SetString("UserLogged", JsonSerializer.Serialize(userLogged));

                    #region Como usar las variables de sesión

                    //Se deserealiza el objeto "UserLogged" que esta guardada como string al tipo "Settings"
                    Settings settings = JsonSerializer.Deserialize<Settings>(HttpContext.Session.GetString("UserLogged"));

                    //Para despues poder utilizarlas con "settings.NombrePropiedad"
                    ViewBag.User = settings.NombreUsuario;
                    ViewBag.Rol = settings.RolID;
                    #endregion

                    AccessModel access = new AccessModel
                    {
                        Success = true,
                        Message = $"Bienvenido {settings.NombreUsuario}",
                        Data = null
                    };

                    return access;
                }
                else
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    AccessModel access = new AccessModel
                    {
                        Success = false,
                        Message = jsonResponse,
                        Data = null
                    };
                    return access;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult LogOut()
        {
            Settings settings = JsonSerializer.Deserialize<Settings>(HttpContext.Session.GetString("UserLogged"));

            settings = null;

            return RedirectToAction("Index");
        }
        #endregion

    }
}


