using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface ISession
    {
        FileCheckerUserArgs UserArgs { get; set; }
    }
}