using System;
using System.IO;
using Anotar.Log4Net;
using FileChecker.Entities;
using Newtonsoft.Json;

namespace FileChecker.Services
{
    public class JsonSettingsLoader : ISettingsProvider<ComparisonSettings>
    {
        public ComparisonSettings GetStoredSettings(string settingsLocation)
        {
            // deserialize our JSON format settings file
            try
            {
                return JsonConvert.DeserializeObject<ComparisonSettings>(File.ReadAllText(settingsLocation));
            }
            catch (Exception ex)
            {
                LogTo.FatalException("Failed to load settings file: " + settingsLocation, ex);
                throw;
            }
        }
    }
}
