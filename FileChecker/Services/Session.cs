using FileChecker.Entities;

namespace FileChecker.Services
{
    public class Session : ISession
    {
        public FileCheckerUserArgs UserArgs { get; set; }

    }
}