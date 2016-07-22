using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using Anotar.Log4Net;
using FileChecker.Entities;

namespace FileChecker.Services.ResultOutputters
{
    public class ResultsFileWriter : IOutputResults
    {
        private readonly ComparisonSettings _settings;

        public ResultsFileWriter(ComparisonSettings settings)
        {
            Validate(settings.ResultsOutputFile);

            _settings = settings;
        }

        private void Validate(string resultsOutputFile)
        {
            if (String.IsNullOrEmpty(resultsOutputFile))
                throw new ArgumentNullException("resultsOutputFile", "Results Output file is required!");
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


            AppendMessageToFile(sb.ToString());

            LogTo.Info("Writing Results - End");
        }

        public void OutputFolderDiffs(IList<FileItem> fileList, string title)
        {
            var sb = new StringBuilder();
            sb.AppendLine(title);

            foreach (var fileItem in fileList)
                sb.AppendLine(fileItem.FileNamePartForComparison);

            AppendMessageToFile(sb.ToString());
        }

        public void AddErrorMessage(string message)
        {
            AppendMessageToFile(message);
        }

        public void AddTitle(string message)
        { 
            AppendMessageToFile(message + Environment.NewLine + Environment.NewLine);
        }

        private void AppendMessageToFile(string message)
        {
            try
            {
                File.AppendAllText(_settings.ResultsOutputFile, message + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // just log this without rethrowing it because a) there might be  no one to handle it
                // and b) any issues with the log file would have been previously highlighted to the log / user
                LogTo.FatalException("Failed to write to output file [" + _settings.ResultsOutputFile + "]", ex);
            }
        }

    }
}
