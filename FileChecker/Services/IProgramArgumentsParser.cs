namespace FileChecker.Services
{
    public interface IProgramArgumentsParser
    {
       
        string GetSettingsFileLocation(string[] args);
    }
}