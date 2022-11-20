using Newtonsoft.Json;
using System;

namespace Sys.Common.Extensions
{
    public class CustomExceptionExtension : Exception
    {
        public string Code { get; set; }

        public CustomExceptionExtension(string code, string message = null) : base(message != null ? message : code)
        {
            Code = code;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new
            {
                Code,
                Message
            });
        }
    }
}