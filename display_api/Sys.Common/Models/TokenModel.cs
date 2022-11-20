namespace Sys.Common.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Expired { get; set; }
        public string TokenType { get; set; }
        public string SecretKey { get; set; }
    }
}