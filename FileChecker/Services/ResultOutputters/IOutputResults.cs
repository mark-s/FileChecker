using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services.ResultOutputters
{
    public interface IOutputResults
    {
        void DeleteExistingResults();
        void ProduceDiff(IList<FilePair> filePairs, bool onlyShowDifferences, string title);
        void ProduceListOfFiles(IList<FileItem> fileList, string title);
    }
}