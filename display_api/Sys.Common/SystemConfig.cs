using System.Collections.Generic;

namespace Sys.Common
{
    internal static class SystemConfig
    {
        public const string EncryptPass = "nProgrammer@$^194";
        public const string SwaggerName = "1Solution System - RDOS TMK Display";

        public static List<string> CORS = new List<string>()
        {
            "*"
        };

        public const string CorsName = "nProx Origin";

        #region JWT

        public const string JWT_KEY = "sw8lnp04kZ0XvLF9NeBg";
        public const int JWT_ACC_EXPIRE = 230;
        public const int JWT_REF_EXPIRE = 43200;

        #endregion JWT
    }
}