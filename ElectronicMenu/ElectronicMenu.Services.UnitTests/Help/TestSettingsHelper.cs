using ElectronicMenu.Services.Settings;

namespace FitnessClub.Service.UnitTests.Helpers;

public static class TestSettingsHelper
{
    public static ElectronicMenuSettings GetSettings()
    {
        return ElectronicMenuSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}