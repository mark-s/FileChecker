using System;
using System.Linq;

namespace FileChecker.Entities
{
    public class FilePair
    {
        public FileItem LeftFile { get; set; }
        public FileItem RightFile { get; set; }


        public byte[] LeftFileHash { get; set; }
        public byte[] RightFileHash { get; set; }


        public string LeftFileHashString
        {
            get { return BitConverter.ToString(LeftFileHash); }
        }

        public string RightFileHashString
        {
            get { return BitConverter.ToString(RightFileHash); }
        }

        public bool AreHashesEqual
        {
            get { return LeftFileHash.SequenceEqual(RightFileHash); }
        }

    }
}
