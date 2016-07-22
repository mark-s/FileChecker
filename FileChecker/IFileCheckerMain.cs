using FileChecker.Entities;
using FileChecker.Services;

namespace FileChecker
{
    public interface IFileCheckerMain
    {
        void RunFileCheck(ComparisonSettings settings, IResultsOutputService outputService);
    }
}