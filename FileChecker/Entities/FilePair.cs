using System.Linq;

namespace FileChecker.Entities
{
    public class FilePair
    {
        public FileItem LeftFile { get; private set; }
        public FileItem RightFile { get; private set; }

        public bool AreHashesEqual
        {
            get { return LeftFile.FileHash.SequenceEqual(RightFile.FileHash); }
        }


        public FilePair(FileItem leftFile, FileItem rightFile)
        {
            LeftFile = leftFile;
            RightFile = rightFile;
        }
    }
}
