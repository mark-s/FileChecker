using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services.ResultOutputters
{
    public interface IOutputResults
    {
        void RemoveExistingResults();

        void OutputFileContentDiffs(IList<FilePair> filePairs, bool onlyShowDifferences, string title);

        void OutputFolderDiffs(IList<FileItem> fileList, string title);

        void AddErrorMessage(string message);

        void AddTitle(string message);
    }
}