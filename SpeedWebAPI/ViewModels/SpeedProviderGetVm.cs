
namespace SpeedWebAPI.ViewModels
{
    public class SpeedProviderGetVm
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public int? ProviderType { get; set; } = 1;
    }
}
