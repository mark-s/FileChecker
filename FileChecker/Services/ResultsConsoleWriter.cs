using System;
using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class ResultsConsoleWriter : IOutputResults
    {

        public void ProduceResults(IList<FilePair> filePairs)
        {

            foreach (var filePair in filePairs)
            {
                Console.WriteLine(String.Format("{0}\t{1}", filePair.LeftFile.FileInfo.FullName, filePair.RightFile.FileInfo.FullName));
                Console.WriteLine("Are Equal:\t" + filePair.AreHashesEqual);
            }

        }


    }
}
