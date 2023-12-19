namespace ElectronicMenu.Services.Settings
{
    public class ElectronicMenuSettingsReader
    {
        public static ElectronicMenuSettings Read(IConfiguration configuration)
        {
            //здесь будет чтение настроек приложения из конфига
            return new ElectronicMenuSettings()
            {
                ServiceUri = configuration.GetValue<Uri>("Uri"),
                ElectronicMenuDbContextConnectionString = configuration.GetValue<string>("ElectronicMenuDbContext"),
                IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
                ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
                ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            };
        }
    }
}
