using FirebaseAdmin.Messaging;
using Sys.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Common.Helper
{
    public interface IFirebaseHelper
    {
        Task<BatchResponse> SendNotificationMultiDevices(string title, string notificationBody, List<string> tokens, Dictionary<string, string> data);

        Task<string> SendNotificationSingleDevice(string title, string notificationBody, string token, Dictionary<string, string> data);
    }
}