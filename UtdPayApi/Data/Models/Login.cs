namespace UtdPayApi.Data.Models
{
    public class LoginRequestModel
    {
        public string ApiKey { get; set; }
        public string Email { get; set; }
        public string Lang { get; set; }
    }

    public class LoginResponseModel
    {
        public bool fail { get; set; }
        public int statusCode { get; set; }
        public LoginResult result { get; set; }
        public int count { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
    }

    public class LoginResult
    {
        public int userId { get; set; }
        public string token { get; set; }
    }

}