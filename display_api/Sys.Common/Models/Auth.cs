using System.ComponentModel.DataAnnotations;

namespace Sys.Common
{
    public partial class Auth
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    // device Info
    public partial class Auth
    {
        public string DeviceName { get; set; }
        public string DeviceId { get; set; }
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public string AppName { get; set; }
        public string AppId { get; set; }
        public string AppVersion { get; set; }
        public string FirebaseToken { get; set; }
        public bool? OTP { get; set; }
    }
}