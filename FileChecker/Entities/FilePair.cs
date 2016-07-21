using System;
using System.Linq;

namespace FileChecker.Entities
{
    public class FilePair
    {
        public FileItem LeftFile { get; private set; }
        public FileItem RightFile { get; private set; }

        public bool AreHashesEqual
        {
            get
            {
                return LeftFile.FileContentHash.SequenceEqual(RightFile.FileContentHash);
            }
        }


        public FilePair(FileItem leftFile, FileItem rightFile)
        {
            if (leftFile == null) throw new ArgumentNullException("leftFile", "Required!");
            if (rightFile == null) throw new ArgumentNullException("rightFile", "Required!");

            LeftFile = leftFile;
            RightFile = rightFile;
        }
    }
}
