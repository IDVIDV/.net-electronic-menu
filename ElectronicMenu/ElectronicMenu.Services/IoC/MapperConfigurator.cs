using ElectronicMenu.BL.Mapper;
using ElectronicMenu.Services.Mapper;

namespace ElectronicMenu.Services.IoC
{
    public class MapperConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<PositionsBLProfile>();
                config.AddProfile<UsersBLProfile>();
                config.AddProfile<PositionsServiceProfile>();
                config.AddProfile<UsersServiceProfile>();
            });
        }
    }
}
