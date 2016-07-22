using System;

namespace FileChecker.Services
{
    public interface ITimeStampService
    {
        string GetTimeStamp();
    }

    public class TimeStampService : ITimeStampService
    {
        public string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd dddd HH:mm:ss") +"  " + TimeZone.CurrentTimeZone.StandardName;
        }

    }
}
