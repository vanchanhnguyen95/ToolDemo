using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerService.Models
{
    public class RouteSpeedProviderData
    {
        int statusCode { get; set; } = 0;
        string messages { get; set; } = "Error Unknown";
        List<SpeedProvider>? datas { get; set; }  = null;
    }
}
