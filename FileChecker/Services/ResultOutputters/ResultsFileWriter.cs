using System;
using System.Collections.Generic;
using System.IO;
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

        public void ProduceResults(IList<FilePair> filePairs)
        {
            LogTo.Info("Writing Results to\t" + _session.Settings.ResultsOutputFilePath);

            var sb = new StringBuilder();
            sb.AppendLine("Is Same\tLeft File\tRight File\tLeft Hash\tRight Hash");
            foreach (var filePair in filePairs)
            {
                sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                                                            filePair.AreHashesEqual,
                                                            filePair.LeftFile.FileInfo.FullName,
                                                            filePair.RightFile.FileInfo.FullName,
                                                            filePair.LeftFileHashString,
                                                            filePair.RightFileHashString));
            }

            File.WriteAllText(_session.Settings.ResultsOutputFilePath, sb.ToString(), Encoding.UTF8);

            LogTo.Info("Writing Results - End");
        }
    }
}
