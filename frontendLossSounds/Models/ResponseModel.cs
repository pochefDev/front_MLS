namespace frontendLossSounds.Models
{
    public class ResponseModel
    {
    }

    public class AccessModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public LoginResponseModel Data { get; set; }
    }
}
