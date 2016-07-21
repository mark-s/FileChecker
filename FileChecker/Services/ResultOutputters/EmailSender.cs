using System;
using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services.ResultOutputters
{
    public class EmailSender : IOutputResults
    {
        private readonly ComparisonSettings _settings;

        public EmailSender(ComparisonSettings settings)
        {
            _settings = settings;
        }

        public void RemoveExistingResults()
        {
            throw new NotImplementedException();
        }

        public void OutputFileContentDiffs(IList<FilePair> filePairs, bool onlyShowDifferences, string title)
        {
            throw new NotImplementedException();
        }

        public void OutputFolderDiffs(IList<FileItem> fileList, string title)
        {
            throw new NotImplementedException();
        }
    }
}