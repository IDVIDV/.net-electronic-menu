using ElectronicMenu.DataAccess;
using ElectronicMenu.WebAPI.Settings;
using Microsoft.EntityFrameworkCore;

namespace ElectronicMenu.WebAPI.IoC
{
    public class DbContextConfigurator
    {
        public static void ConfigureService(IServiceCollection services, ElectronicMenuSettings settings)
        {
            services.AddDbContextFactory<ElectronicMenuDbContext>(
                options => { options.UseSqlServer(settings.ElectronicMenuDbContextConnectionString); },
                ServiceLifetime.Scoped);
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ElectronicMenuDbContext>>();
            using var context = contextFactory.CreateDbContext();
            context.Database.Migrate(); //makes last migrations to db and creates database if it doesn't exist
        }
    }
}
