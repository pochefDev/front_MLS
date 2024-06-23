namespace frontendLossSounds.Models
{
    public class UserModel
    {
    }

    public class GetUserResponse
    {
        public int ID_USUARIO { get; set; }
        public string Nombre_Usuario { get; set; }
        public int ID_ROL { get; set; }
        public string Rol { get; set; }

        public bool Active { get; set; }
    }


    public class EditRequestUser
    {
        public string Nombre_Usuario { get; set; }
        public int ID_Rol { get; set; }
        public bool Active { get; set; }
    }

    public class RolesResponseModel
    {
        public int ID_Rol { get; set; }
        public string Rol { get; set; }
        public string Description { get; set; }
    }

    public static class Role
    {
        public const string IT_ADMIN = "IT_ADMIN";
        public const string ADMIN = "ADMIN";
        public const string USER = "USER";
    }
}