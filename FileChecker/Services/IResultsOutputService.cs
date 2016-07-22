using FileChecker.Entities;
using FileChecker.Services.ResultOutputters;

namespace FileChecker.Services
{
    public interface IResultsOutputService
    {
        void AddOutputter(IOutputResults outputter);
        void OutputResults(ComparisonResults comparisonResults, ComparisonSettings settings);
        void OutputError(string message);
    }
}