using System;

namespace Sys.Common.Models
{
    public class FirebaseModel
    {
        public DynamicLinkInfo dynamicLinkInfo { get; set; }
    }

    public class DynamicLinkInfo
    {
        public string domainUriPrefix { get; set; }
        public string link { get; set; }
        public AndroidInfo androidInfo { get; set; }
        public IosInfo iosInfo { get; set; }
    }

    public class AndroidInfo
    {
        public string androidPackageName { get; set; }
    }

    public class IosInfo
    {
        public string iosBundleId { get; set; }
    }

    public class FirebaseResponseModel
    {
        public string ShortLink { get; set; }
        public object Warning { get; set; }
        public string PreviewLink { get; set; }
    }
}
