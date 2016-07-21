using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Anotar.Log4Net;
using FileChecker.Entities;

namespace FileChecker.Services.ResultOutputters
{
    public class ResultsFileWriter : IOutputResults
    {
        private readonly ComparisonSettings _settings;

        public ResultsFileWriter(ComparisonSettings settings)
        {
            _settings = settings;
        }


        public void RemoveExistingResults()
        {
            if (File.Exists(_settings.ResultsOutputFile))
                File.Delete(_settings.ResultsOutputFile);
        }

        public void OutputFileContentDiffs(IList<FilePair> filePairs, bool onlyShowDifferences, string title)
        {
            LogTo.Info("Writing Results to\t" + _settings.ResultsOutputFile);

            var sb = new StringBuilder();
            sb.AppendLine(title);
            sb.AppendLine("Is Same\tLeft File\tRight File\tLeft Hash\tRight Hash");

            if (onlyShowDifferences)
                foreach (var filePair in filePairs.Where(fp => fp.AreHashesEqual == false))
                {
                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                                                                filePair.AreHashesEqual,
                                                                filePair.LeftFile.FullName,
                                                                filePair.RightFile.FullName,
                                                                filePair.LeftFile.FileContentHashString,
                                                                filePair.RightFile.FileContentHashString));
                }
            else
                foreach (var filePair in filePairs)
                {
                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                                                                filePair.AreHashesEqual,
                                                                filePair.LeftFile.FullName,
                                                                filePair.RightFile.FullName,
                                                                filePair.LeftFile.FileContentHashString,
                                                                filePair.RightFile.FileContentHashString));
                }


            File.AppendAllText(_settings.ResultsOutputFile, sb.ToString(), Encoding.UTF8);

            LogTo.Info("Writing Results - End");
        }

        public void OutputFolderDiffs(IList<FileItem> fileList, string title)
        {
            var sb = new StringBuilder();
            sb.AppendLine(title);

            foreach (var fileItem in fileList)
                sb.AppendLine(fileItem.FileNamePartForComparison);

            File.AppendAllText(_settings.ResultsOutputFile, sb.ToString(), Encoding.UTF8);
        }
    }
}
