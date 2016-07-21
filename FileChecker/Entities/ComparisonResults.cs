using System.Collections.Generic;

namespace FileChecker.Entities
{
    public class ComparisonResults
    {
        public IList<FilePair> FilePairs { get; private set; }
        public IList<FileItem> FilesOnlyInLeft { get; private set; }
        public IList<FileItem> FilesOnlyInRight { get; private set; }

        public ComparisonResults(IList<FilePair> filePairs, IList<FileItem> filesOnlyInLeft, IList<FileItem> filesOnlyInRight)
        {
            FilePairs = filePairs;
            FilesOnlyInLeft = filesOnlyInLeft;
            FilesOnlyInRight = filesOnlyInRight;
        }


    }
}