namespace frontendLossSounds.Models
{
    public class LoginModel
    {
        public string Nombre_Usuario { get; set; }
        public string Contrasena { get; set; }

    }
    partial class AppSettings
    {
        public string UrlBaseApi{ get; set; }

    }
}
