using System.Linq;

namespace FileChecker.Entities
{
    public class FilePair
    {
        public FileItem LeftFile { get; set; }
        public FileItem RightFile { get; set; }

        public bool AreHashesEqual
        {
            get { return LeftFile.FileHash.SequenceEqual(RightFile.FileHash); }
        }

    }
}
