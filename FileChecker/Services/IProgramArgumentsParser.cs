using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IProgramArgumentsParser
    {
        ComparisonSettings ParseArgs(string[] args);
    }
}