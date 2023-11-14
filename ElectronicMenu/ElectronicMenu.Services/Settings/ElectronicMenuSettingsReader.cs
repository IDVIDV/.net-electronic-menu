namespace ElectronicMenu.Services.Settings
{
    public class ElectronicMenuSettingsReader
    {
        public static ElectronicMenuSettings Read(IConfiguration configuration)
        {
            //здесь будет чтение настроек приложения из конфига
            return new ElectronicMenuSettings()
            {
                ElectronicMenuDbContextConnectionString = configuration.GetValue<string>("ElectronicMenuDbContext")
            };
        }
    }
}
