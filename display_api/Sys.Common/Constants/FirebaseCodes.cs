namespace Sys.Common.Constants
{
    public static class FirebaseCodes
    {
        public static class Types
        {
            public const string LOGOUT_DEVICE = "LOGOUT_DEVICE";
        }

        public static class Actions
        {
            public const string FLUTTER_NOTIFICATION_CLICK = "FLUTTER_NOTIFICATION_CLICK";
        }

        public class DataFireBase
        {
            public string Type { get; set; }

            public string Click_Action { get; set; }
        }
    }
}