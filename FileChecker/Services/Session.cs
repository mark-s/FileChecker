using FileChecker.Entities;

namespace FileChecker.Services
{
    public class Session : ISession
    {
        public ComparisonSettings Settings { get; set; }

    }
}