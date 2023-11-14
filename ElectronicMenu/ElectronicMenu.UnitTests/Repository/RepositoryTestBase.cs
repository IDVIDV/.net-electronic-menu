using ElectronicMenu.DataAccess;
using ElectronicMenu.Services.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicMenu.UnitTests.Repository
{
    public class RepositoryTestsBase
    {
        protected readonly ElectronicMenuSettings Settings;
        protected readonly IDbContextFactory<ElectronicMenuDbContext> DbContextFactory;
        protected readonly IServiceProvider ServiceProvider;
        public RepositoryTestsBase()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            Settings = ElectronicMenuSettingsReader.Read(configuration);
            ServiceProvider = ConfigureServiceProvider();

            DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<ElectronicMenuDbContext>>();
        }

        private IServiceProvider ConfigureServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContextFactory<ElectronicMenuDbContext>(
                options => { options.UseSqlServer(Settings.ElectronicMenuDbContextConnectionString); },
                ServiceLifetime.Scoped);
            return serviceCollection.BuildServiceProvider();
        }
    }
}
