using System;
using System.Collections.Generic;
using System.Linq;
using FileChecker.Entities;

namespace FileChecker.Services.ResultOutputters
{
    public class ResultsConsoleWriter : IOutputResults
    {


        public void DeleteExistingResults()
        {
            Console.Clear();
        }

        public void ProduceDiff(IList<FilePair> filePairs, bool onlyShowDifferences, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine();

            if (onlyShowDifferences)
                foreach (var filePair in filePairs.Where(fp => fp.AreHashesEqual == false))
                {
                    Console.WriteLine(String.Format("{0}\t{1}", filePair.LeftFile.FullName, filePair.RightFile.FullName));
                    Console.WriteLine("Are Equal:\t" + filePair.AreHashesEqual);
                }
            else
                foreach (var filePair in filePairs)
                {
                    Console.WriteLine(String.Format("{0}\t{1}", filePair.LeftFile.FullName, filePair.RightFile.FullName));
                    Console.WriteLine("Are Equal:\t" + filePair.AreHashesEqual);
                }
        }

        public void ProduceListOfFiles(IList<FileItem> fileList, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine();

            foreach (var fileItem in fileList)
                Console.WriteLine(fileItem.FullName);
        }
    }
}
