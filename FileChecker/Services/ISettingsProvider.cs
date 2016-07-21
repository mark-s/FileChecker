namespace FileChecker.Services
{
    public interface ISettingsProvider<out T>
    {
        T GetStoredSettings(string settingsLocation);
    }
}