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
        private readonly ISession _session;

        public ResultsFileWriter(ISession session)
        {
            _session = session;
        }


        public void DeleteExistingResults()
        {
            if (File.Exists(_session.Settings.ResultsOutputFilePath))
                File.Delete(_session.Settings.ResultsOutputFilePath);
        }

        public void ProduceDiff(IList<FilePair> filePairs, bool onlyShowDifferences, string title)
        {
            LogTo.Info("Writing Results to\t" + _session.Settings.ResultsOutputFilePath);

            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(title);
            sb.AppendLine("Is Same\tLeft File\tRight File\tLeft Hash\tRight Hash");

            if (onlyShowDifferences)
                foreach (var filePair in filePairs.Where(fp => fp.AreHashesEqual == false))
                {
                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                                                                filePair.AreHashesEqual,
                                                                filePair.LeftFile.FullName,
                                                                filePair.RightFile.FullName,
                                                                filePair.LeftFile.FileHashString,
                                                                filePair.RightFile.FileHashString));
                }
            else
                foreach (var filePair in filePairs)
                {
                    sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                                                                filePair.AreHashesEqual,
                                                                filePair.LeftFile.FullName,
                                                                filePair.RightFile.FullName,
                                                                filePair.LeftFile.FileHashString,
                                                                filePair.RightFile.FileHashString));
                }


            File.AppendAllText(_session.Settings.ResultsOutputFilePath, sb.ToString(), Encoding.UTF8);

            LogTo.Info("Writing Results - End");
        }

        public void ProduceListOfFiles(IList<FileItem> fileList, string title)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine(title);

            foreach (var fileItem in fileList)
                sb.AppendLine(fileItem.FileNamePartForComparison);

            File.AppendAllText(_session.Settings.ResultsOutputFilePath, sb.ToString(), Encoding.UTF8);
        }
    }
}
