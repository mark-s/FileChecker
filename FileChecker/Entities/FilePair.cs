using System.Linq;

namespace FileChecker.Entities
{
    public class FilePair
    {
        public FileItem LeftFile { get; set; }
        public FileItem RightFile { get; set; }

        public byte[] LeftFileHash { get; set; }
        public byte[] RightFileHash { get; set; }

        public bool AreHashesEqual
        {
            get { return CompareHashes(); }
        }


        private bool CompareHashes()
        {
            return LeftFileHash.SequenceEqual(RightFileHash);
        }
    }
}
