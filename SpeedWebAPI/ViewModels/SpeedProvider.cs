namespace SpeedWebAPI.ViewModels
{
    public class SpeedProvider
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int? ProviderType { get; set; } = 1;
    }
}
