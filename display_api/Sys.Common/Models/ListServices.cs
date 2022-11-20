using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;

namespace Sys.Common.Models
{
    public partial class ListServices
    {
        public IApplicationBuilder App { get; set; }
        public IWebHostEnvironment Env { get; set; }
        public IApiVersionDescriptionProvider Provider { get; set; }
        public ILoggerFactory logFactory { get; set; }
    }
}