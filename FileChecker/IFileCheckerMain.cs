using FileChecker.Entities;

namespace FileChecker
{
    public interface IFileCheckerMain
    {
        void Go(ComparisonSettings settings);
    }
}