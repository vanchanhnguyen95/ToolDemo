using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.FirebaseDynamicLinks.v1;
using Google.Apis.FirebaseDynamicLinks.v1.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Common.Helper
{
    public class FirebaseHelper : IFirebaseHelper
    {
        private readonly FirebaseMessaging messaging;
        private readonly FirebaseDynamicLinksService _firebaseDynamicLinksService;
        private readonly ILogger _logger;

        public FirebaseHelper(ILogger<FirebaseHelper> logger)
        {
            _logger = logger;

            var app = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase.json")),
            });
            messaging = FirebaseMessaging.GetMessaging(app);
        }

        #region Single device

        private Message CreateNotificationSingleDevice(string title, string notificationBody, string token, Dictionary<string, string> data)
        {
            return new Message()
            {
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title,
                },
                Data = data
            };
        }

        public async Task<string> SendNotificationSingleDevice(string title, string notificationBody, string token, Dictionary<string, string> data)
        {
            string result = await messaging.SendAsync(CreateNotificationSingleDevice(title, notificationBody, token, data));
            _logger.LogInformation($"[{DateTime.Now}] [ERR] \r\nRequest token: {token}\r\nRequest body: {notificationBody}\r\nError message: {result}");
            return result;
        }

        #endregion Single device

        #region Multi devices

        private MulticastMessage CreateNotificationMultiDevices(string title, string notificationBody, List<string> tokens, Dictionary<string, string> data)
        {
            return new MulticastMessage()
            {
                Tokens = tokens,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                },
                Data = data
            };
        }

        public async Task<BatchResponse> SendNotificationMultiDevices(string title, string notificationBody, List<string> tokens, Dictionary<string, string> data)
        {
            var result = await messaging.SendMulticastAsync(CreateNotificationMultiDevices(title, notificationBody, tokens, data));
            if (result.FailureCount > 0)
            {
                for (var i = 0; i < result.Responses.Count; i++)
                {
                    if (!result.Responses[i].IsSuccess)
                    {
                        _logger.LogError($"[{DateTime.Now}] [ERR] \r\nMessage ID: {result.Responses[i].MessageId}\r\nRequest body: {notificationBody}");
                    }
                }
            }
            return result;
        }

        #endregion Multi devices

        //public async Task<string> CreateDynamicLinkFirebase(DynamicLinkModel model)
        //{
        //    CreateShortDynamicLinkRequest shortDynamicLinkRequest = new CreateShortDynamicLinkRequest()
        //    {
        //        DynamicLinkInfo = new DynamicLinkInfo()
        //        {
        //            AndroidInfo = new AndroidInfo() { AndroidPackageName = model.AndroidPackageName },
        //            IosInfo = new IosInfo() { IosAppStoreId = model.IosAppStoreId, IosBundleId = model.IosBundleId },
        //            DomainUriPrefix = $"https://{model.DomainUriPrefix}",
        //            Link = $"https://www.example.com/?{model.QueryString}",
        //        },
        //        Suffix = new Suffix() { Option = "SHORT" }
        //    };

        //    shortdynamiclinkrequest.dynamiclinkinfo = new dynamiclinkinfo();
        //    shortdynamiclinkrequest.dynamiclinkinfo.androidinfo = new androidinfo() { androidpackagename = model.androidpackagename };
        //    shortdynamiclinkrequest.dynamiclinkinfo.iosinfo = new iosinfo() { iosappstoreid = model.iosappstoreid, iosbundleid = model.iosbundleid };
        //    shortdynamiclinkrequest.dynamiclinkinfo.domainuriprefix = $"https://{model.domainuriprefix}";
        //    shortdynamiclinkrequest.dynamiclinkinfo.link = $"https://www.example.com/?{model.querystring}";
        //    shortdynamiclinkrequest.suffix = new suffix() { option = "short" };
        //    var request = firebaseDynamicLinksService.ShortLinks.Create(shortDynamicLinkRequest);
        //    return request.Execute().ShortLink;

        //    var json = JsonConvert.SerializeObject(shortDynamicLinkRequest, Formatting.Indented, new CreateShortDynamicLinkRequestConverter());
        //    var request = _firebaseDynamicLinksService.ShortLinks.Create(new CreateShortDynamicLinkRequest());

        //    request.ModifyRequest = message => message.Content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var res = await request.ExecuteAsync();
        //    return res.ShortLink;
        //}
    }
}