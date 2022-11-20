namespace Sys.Common.Models
{
    public class JWTResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string SecretKey { get; set; }
        public string ErrorCode { get; set; }
        public string EmpId { get; set; }
    }
}