using AutoMapper;
using ElectronicMenu.BL.Users;
using ElectronicMenu.DataAccess.Entities;
using ElectronicMenu.DataAccess;
using Microsoft.AspNetCore.Identity;
using ElectronicMenu.BL.Positions;
using ElectronicMenu.Services.Settings;
using ElectronicMenu.BL.Auth;

namespace ElectronicMenu.Services.IoC
{
    public class ServicesConfigurator
    {
        public static void ConfigureService(IServiceCollection services, ElectronicMenuSettings settings)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IPositionsProvider>(x =>
                new PositionsProvider(x.GetRequiredService<IRepository<PositionEntity>>(), x.GetRequiredService<IMapper>()));

            services.AddScoped<IUsersProvider>(x =>
                new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(), x.GetRequiredService<IMapper>()));

            services.AddScoped<IPositionsManager, PositionsManager>();
            services.AddScoped<IUsersManager, UsersManager>();

            services.AddScoped<IAuthProvider>(x =>
                new AuthProvider(x.GetRequiredService<SignInManager<UserEntity>>(),
                    x.GetRequiredService<UserManager<UserEntity>>(),
                    x.GetRequiredService<IHttpClientFactory>(),
                    settings.IdentityServerUri,
                    settings.ClientId,
                    settings.ClientSecret));

        }
    }
}
