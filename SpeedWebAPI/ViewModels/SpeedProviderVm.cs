
namespace SpeedWebAPI.ViewModels
{
    public class SpeedProviderVm
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public int? MinSpeed { get; set; } = 0;
        public int? MaxSpeed { get; set; } = 0;
        public int? ProviderType { get; set; } = 1;
    }
}
