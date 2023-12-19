namespace ElectronicMenu.Services.Settings
{
    public class ElectronicMenuSettings
    {
        public Uri ServiceUri { get; set; }
        public string ElectronicMenuDbContextConnectionString { get; set; }
        public string IdentityServerUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
