using frontendLossSounds.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
        }

        public ResponseData ValidatePassword(string password)
        {
            // Lista de validaciones con los respectivos mensajes de error
            var validations = new List<(Func<string, bool> validation, string errorMessage)>
            {
            (password => password.Length >= 8, "La contraseña debe tener al menos 8 caracteres."),
            (password => password.Any(char.IsUpper), "La contraseña debe tener al menos una letra mayúscula."),
            (password => password.Any(char.IsLower), "La contraseña debe tener al menos una letra minúscula."),
            (password => password.Any(ch => !char.IsLetterOrDigit(ch)), "La contraseña debe tener al menos un carácter especial."),
            (password => !HasConsecutiveNumbers(password), "La contraseña no puede contener números consecutivos."),
            (password => !HasConsecutiveLetters(password), "La contraseña no puede contener letras consecutivas del abecedario.")
            };

            foreach (var (validation, errorMessage) in validations)
            {
                if (!validation(password))
                {
                    return GenerateErrorResponse(errorMessage);
                }
            }

            return new ResponseData
            {
                Success = true,
                Message = "Validación exitosa.",
                Data = null
            };
        }

        private static ResponseData GenerateErrorResponse(string message)
        {
            return new ResponseData
            {
                Success = false,
                Message = message,
                Data = null
            };
        }


        private static bool HasConsecutiveNumbers(string password)
        {
            for (int i = 0; i < password.Length - 1; i++)
            {
                if (char.IsDigit(password[i]) && password[i + 1] == password[i] + 1)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool HasConsecutiveLetters(string password)
        {
            string lowerPassword = password.ToLower();
            for (int i = 0; i < lowerPassword.Length - 1; i++)
            {
                if (char.IsLetter(lowerPassword[i]) && lowerPassword[i + 1] == lowerPassword[i] + 1)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
