using System.Collections.Generic;
using Anotar.Log4Net;
using FileChecker.Entities;
using FileChecker.Services.ResultOutputters;

namespace FileChecker.Services
{
    public interface IResultsOutputService
    {
        void AddOutputter(IOutputResults outputter);
        void OutputResults(ComparisonResults comparisonResults, ComparisonSettings settings);
    }

    public class ResultsOutputService : IResultsOutputService
    {

        private readonly List<IOutputResults> _outputters = new List<IOutputResults>();


        public void AddOutputter(IOutputResults outputter)
        {
            _outputters.Add(outputter);

            LogTo.Info("Added Outputter: " + outputter.GetType().Name);
        }

        public void OutputResults(ComparisonResults comparisonResults, ComparisonSettings settings)
        {
            foreach (var outputter in _outputters)
            {
                outputter.RemoveExistingResults();
                outputter.OutputFileContentDiffs(comparisonResults.FilePairs, settings.OnlyShowDiffs, "File Comparsion");
                outputter.OutputFolderDiffs(comparisonResults.FilesOnlyInLeft, "Files Missing From Right (" + settings.PathToCheckRight + ")");
                outputter.OutputFolderDiffs(comparisonResults.FilesOnlyInRight, "Files Missing From Left (" + settings.PathToCheckLeft + ")");
            }
        }

    }
}
