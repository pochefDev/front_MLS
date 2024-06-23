using frontendLossSounds.Models;
using frontendLossSounds.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace frontendLossSounds.Controllers
{
    public class UserController : Controller
    {
        #region Configurations
        private readonly ILogger<UserController> _logger;

        private BaseServices _services;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _services = new BaseServices();
        }
        #endregion

        #region Index

        [Authorize(Role.IT_ADMIN)]
        public async Task<ActionResult> UserIndex()
        {
            try
            {
                IActionResult response = await GetUsers(null);

                if (response is JsonResult jsonResult)
                {
                    var jsonString = JsonSerializer.Serialize(jsonResult.Value);
                    List<GetUserResponse> users = JsonSerializer.Deserialize<List<GetUserResponse>>(jsonString);

                    return View(users);
                }
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        

        #endregion

        #region Edit

        [Authorize(Role.IT_ADMIN)]
        public async Task<ActionResult> UserEdit(int ID_User)
        {
            try
            {
                IActionResult response = await GetUsers(ID_User);
                IActionResult roles = await GetRoles();

                if (response is JsonResult jsonResult && roles is JsonResult rolesResult)
                {
                    string jsonString = JsonSerializer.Serialize(jsonResult.Value);
                    List<GetUserResponse> users = JsonSerializer.Deserialize<List<GetUserResponse>>(jsonString);

                    string jsonRoles = JsonSerializer.Serialize(rolesResult.Value);
                    List<RolesResponseModel> rolesList = JsonSerializer.Deserialize<List<RolesResponseModel>>(jsonRoles);

                    // Eliminar el rol del usuario de la lista de roles para evitar duplicados
                    foreach (var user in users)
                    {
                        rolesList.RemoveAll(r => r.ID_Rol == user.ID_ROL);
                    }

                    ViewBag.Roles = rolesList.Select(x => new SelectListItem { Text = x.Rol, Value = x.ID_Rol.ToString() });
                    return View(users);
                }
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<ResponseData> UserEdit(int ID_User, EditRequestUser EditUser)
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

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
                string url = $"/api/EditUser?ID_User={ID_User}";

                string jsonContent = JsonSerializer.Serialize(EditUser);

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

        #region Resources

        public async Task<IActionResult> GetUsers(int? ID_User)
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

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
                string url = $"/api/GetUsuarios?ID_User={ID_User}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<List<GetUserResponse>>(jsonResponse);
                    return Json(responseObject);
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

        public async Task<IActionResult> GetRoles()
        {
            try
            {
                TokenModel tokenModel = await _services.GetToken();

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
                string url = $"/api/GetRoles";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<List<RolesResponseModel>>(jsonResponse);
                    return Json(responseObject);
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

        #endregion

    }
}
