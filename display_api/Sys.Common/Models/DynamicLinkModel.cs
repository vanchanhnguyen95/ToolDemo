namespace Sys.Common.Models
{
    public class DynamicLinkModel
    {
        public string AndroidPackageName { get; set; }
        public string IosAppStoreId { get; set; }
        public string IosBundleId { get; set; }
        public string DomainUriPrefix { get; set; }
        public string QueryString { get; set; }
    }
}