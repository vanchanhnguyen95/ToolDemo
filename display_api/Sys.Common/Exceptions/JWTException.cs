using System;
using System.Globalization;

namespace Sys.Common.Exceptions
{
    public class JWTException : Exception
    {
        public JWTException() : base()
        {
        }

        public JWTException(string message) : base(message)
        {
        }

        public JWTException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}