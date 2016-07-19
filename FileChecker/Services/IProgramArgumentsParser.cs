using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IProgramArgumentsParser
    {
        FileCheckerUserArgs ParseArgs(string[] args);
    }
}