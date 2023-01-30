
namespace DemoRedis.Installers
{
    public class SystemInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //throw new NotImplementedException();
            services.AddControllers();
            services.AddMvc();
        }
    }
}
