namespace DemoRedis.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallerServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            // Lấy ra hết tất cả class trong thư mục IInstaller.cs và phải bỏ đi những Interface và Abstract class
            var installer = typeof(Program).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface
            && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installer.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
